using UnityEngine;
using System.Collections.Generic;

namespace ZYC
{
    /// <summary>
    /// 状态类型
    /// </summary>
    public enum StateType
    {
        NENO,
        IDLE,
        MOVE,
        ATK,
        DEAD,

    }

    /// <summary>
    /// 切换状态事件
    /// </summary>
    public enum StateEvent
    {
        EVENT_IDLE,
        EVENT_MOVE,
        EVENT_ATK,
        EVENT_DEAD,
    }

    public class StateBase
    {
        public int RoleID { get; private set;}

        public StateBase(int roleId)
        {
            RoleID = roleId;
        }

        public virtual void OnEnter(int beforeStateId ,params object[] objs){}
        public virtual bool OnExit(int nextStateId , params object[] objs){return true;}
        public virtual void OnUpdate(float delTime){}
    }

    public class StateMechine
    {
        private Dictionary<int, StateBase> stateDic = new Dictionary<int, StateBase>();
        private Dictionary<int, Dictionary<int, int>> stateEventDic = new Dictionary<int, Dictionary<int, int>>();

        private int curStateId = 0;

        /// <summary>
        /// 注册状态事件
        /// </summary>
        /// <param name="stateEvent">State event.</param>
        /// <param name="fromState">From state.</param>
        /// <param name="toState">To state.</param>
        public void RegisterEvent(int stateEvent,int fromState , int toState)
        {
            Dictionary<int, int> dic;
            if(stateEventDic.TryGetValue(stateEvent , out dic))
            {
                if(dic.ContainsKey(fromState))
                {
                    Debug.LogError("has register this event" + stateEvent.ToString() + "from state"+fromState.ToString() + "to state"+toState.ToString());
                    return;
                }
                dic.Add(fromState , toState);
            }
            else
            {
                stateEventDic.Add(stateEvent , dic);
                dic.Add(fromState , toState);
            }
        }

        /// <summary>
        /// 注册状态
        /// </summary>
        /// <param name="stateId">State identifier.</param>
        /// <param name="state">State.</param>
        public void RegisterState(int stateId , StateBase state)
        {
            if (stateDic.ContainsKey(stateId))
                Debug.LogError("stateid " + stateId.ToString() + "重复注册");
            else
            {
                stateDic.Add(stateId , state);
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="stateEvent">State event.</param>
        /// <param name="toStateId">To state identifier.</param>
        /// <param name="objs">Objects.</param>
        public void ActionEvent(StateEvent stateEvent , StateType toStateId , params object[] objs)
        {
            Dictionary<int, int> dic;
            if(!stateEventDic.TryGetValue((int)stateEvent , out dic))
            {
                Debug.LogError("stateEvent" + stateEvent.ToString() + "没有注册");
                return;
            }
            if (!dic.ContainsKey(curStateId))
            {
                Debug.LogError("stateEvent" + stateEvent.ToString() + "不能触发" + "cursateId is " + curStateId.ToString() + "nextStateId is " + toStateId.ToString());
                return;
            }
            if(dic[curStateId] != (int)toStateId)
            {
                Debug.LogError("stateEvent" + stateEvent.ToString() + "不能触发" + "cursateId is " + curStateId.ToString() + "nextStateId is " + toStateId.ToString());
                return;
            }

            ChangeState((int)toStateId , objs);
           
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="toStateId">To state identifier.</param>
        /// <param name="objs">Objects.</param>
        private void ChangeState(int toStateId , params object[] objs)
        {
            if(curStateId!=0)
            {
                StateBase curState;
                if (stateDic.TryGetValue(curStateId, out curState))
                {
                    if (!curState.OnExit(toStateId, objs))
                    {
                        Debug.LogError("当前状态是" + curStateId.ToString() + "不能切换到状态" + toStateId.ToString());
                        return;
                    }
                }
            }

            StateBase nextState;

            if (!stateDic.TryGetValue(toStateId, out nextState))
            {
                Debug.LogError("没有注册状态" + curStateId.ToString());
                return;
            }

            nextState.OnEnter(curStateId, objs);
            curStateId = toStateId;
        }
    }
}

