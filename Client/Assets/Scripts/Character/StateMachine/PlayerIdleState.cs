﻿
using ZYC;

public class PlayerIdleState : StateBase
{
    public PlayerIdleState(int id):base(id)
    {
    }

    public override void OnEnter(int beforeStateId, params object[] objs)
    {
        base.OnEnter(beforeStateId, objs);
    }

    public override void OnUpdate(float delTime)
    {
        base.OnUpdate(delTime);
    }

    public override bool OnExit(int nextStateId, params object[] objs)
    {
        return base.OnExit(nextStateId, objs);
    }

}
