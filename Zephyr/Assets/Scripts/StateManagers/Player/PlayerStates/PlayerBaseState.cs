using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    protected Core core;

    protected int xInput;
    protected int yInput;

    protected bool isTouchingCeiling;
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;


    protected Movement Movement
    {
        get => movement ?? core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    protected CollisionSenses CollisionSenses
    {
        get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
    }

    private CollisionSenses collisionSenses;

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
        InitializeSubstate();
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
        Debug.Log(animBoolName);
    }
    public virtual void ExitState()
    {
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }
    public abstract void InitializeSubstate();
    public virtual void DoChecks()
    {
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;

        if (CollisionSenses)
        {
            isGrounded = player.Ground.GetOnGround();
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
            isTouchingCeiling = CollisionSenses.Ceiling;
        }
    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

}
