using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseState
{
    public abstract void EnterState();
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void ExitState();
    public abstract void DoChecks();
    public abstract void AnimationTrigger();
    public abstract void AnimationFinishTrigger();
    public abstract void SetPrevState(PlayerBaseState prevState);

}
