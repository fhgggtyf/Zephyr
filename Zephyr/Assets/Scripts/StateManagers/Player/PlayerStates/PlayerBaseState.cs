using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    protected Core core;

    private bool _isRootState = false;

    protected Player player;

    protected PlayerData playerData;

    private PlayerStateMachine _stateMachine;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;

    public bool IsRootState { get => _isRootState; set => _isRootState = value; }
    public PlayerStateMachine StateMachine { get => _stateMachine; set => _stateMachine = value; }

    public PlayerBaseState(Player currentContext, string animBoolName) 
    {
        StateMachine = new PlayerStateMachine();
        this.player = currentContext;
        this.playerData = currentContext.PlayerData;
        this.animBoolName = animBoolName;
        core = currentContext.Core;
        StateMachine.SetCurrentState(this);
        StateMachine.Ctx = player;
    }

    public virtual void EnterState()
    {
        DoChecks();
        //player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        //Debug.Log(animBoolName);
        isAnimationFinished = false;
        isExitingState = false;
    }
    public abstract void LogicUpdate();
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void ExitState()
    {
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }
    public abstract void InitializeSubstate();
    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

}
