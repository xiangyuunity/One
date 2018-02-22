using UnityEngine;
using System.Collections.Generic;
namespace ZYC
{
    public class MyPlayer : PlayerBase
    {
        public override void Init(GameObject obj, int id)
        {
            base.Init(obj, id);
            roleObj.transform.position = Vector3.zero;
            RoleMgr.Instance.MoveEvent += MoveEvent;
        }

        void MoveEvent(Vector2 dir)
        {
            
        }

    }
}


