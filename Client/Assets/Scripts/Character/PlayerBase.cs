using UnityEngine;
using System.Collections;

namespace ZYC
{
    public class PlayerBase
    {
        protected GameObject roleObj;
        protected int roleId;
        protected StateMechine fsm;

        public virtual void Init(GameObject obj, int id)
        {
            roleObj = obj;
            roleId = id;

            fsm = PlayerStateMgr.Instance.Create(id, RoleType.ROLE);
            fsm.ActionEvent(StateEvent.EVENT_IDLE,StateType.IDLE);
        }
    }

}

