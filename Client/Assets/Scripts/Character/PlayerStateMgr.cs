
using System.Collections.Generic;

namespace ZYC
{

    public enum RoleType
    {
        ROLE,
        ROBET,
    }
    /// <summary>
    /// 静态创建函数
    /// </summary>
    public class PlayerStateMgr : AESingleton<PlayerStateMgr>
    {
        public StateMechine Create(int id, RoleType roleType)
        {
            StateMechine stateMechine = new StateMechine();
            CreateRoleState(id, stateMechine);
            return stateMechine;
        }

        private void CreateRoleState(int id, StateMechine stateMechine)
        {
            PlayerIdleState idleState = new PlayerIdleState(id);
            PlayerDeadState deadState = new PlayerDeadState(id);
            PlayerMoveState moveState = new PlayerMoveState(id);
            PlayerAtkState atkState = new PlayerAtkState(id);

            stateMechine.RegisterState((int)StateType.IDLE, idleState);
            stateMechine.RegisterState((int)StateType.DEAD, deadState);
            stateMechine.RegisterState((int)StateType.MOVE, moveState);
            stateMechine.RegisterState((int)StateType.ATK, atkState);

            stateMechine.RegisterEvent((int)StateEvent.EVENT_IDLE, (int)StateType.NENO, (int)StateType.IDLE);
            stateMechine.RegisterEvent((int)StateEvent.EVENT_IDLE, (int)StateType.IDLE, (int)StateType.IDLE);
            stateMechine.RegisterEvent((int)StateEvent.EVENT_IDLE, (int)StateType.ATK, (int)StateType.IDLE);
            stateMechine.RegisterEvent((int)StateEvent.EVENT_IDLE, (int)StateType.MOVE, (int)StateType.IDLE);

            stateMechine.RegisterEvent((int)StateEvent.EVENT_DEAD, (int)StateType.IDLE, (int)StateType.DEAD);
            stateMechine.RegisterEvent((int)StateEvent.EVENT_DEAD, (int)StateType.ATK, (int)StateType.DEAD);
            stateMechine.RegisterEvent((int)StateEvent.EVENT_DEAD, (int)StateType.MOVE, (int)StateType.DEAD);

            stateMechine.RegisterEvent((int)StateEvent.EVENT_MOVE, (int)StateType.IDLE, (int)StateType.MOVE);
            stateMechine.RegisterEvent((int)StateEvent.EVENT_MOVE, (int)StateType.ATK, (int)StateType.MOVE);

            stateMechine.RegisterEvent((int)StateEvent.EVENT_ATK, (int)StateType.IDLE, (int)StateType.ATK);
            stateMechine.RegisterEvent((int)StateEvent.EVENT_ATK, (int)StateType.MOVE, (int)StateType.ATK);
            stateMechine.RegisterEvent((int)StateEvent.EVENT_ATK, (int)StateType.ATK, (int)StateType.ATK);

        }
    }

}





