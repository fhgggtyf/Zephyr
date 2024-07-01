using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : IBaseState
{
    private Core _core;

    protected int xInput;
    protected int yInput;

    protected bool isTouchingCeiling;
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;

    PlayerBaseState _currentSubState;
    PlayerBaseState _currentSuperState;
    PlayerBaseState _prevState;

    private Player _player;

    protected Movement Movement
    {
        get => movement ?? _core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    protected CollisionSenses CollisionSenses
    {
        get => collisionSenses ?? _core.GetCoreComponent(ref collisionSenses);
    }

    private CollisionSenses collisionSenses;

    private bool _isRootState = false;

    protected PlayerStateMachine _ctx;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string _animBoolName;

    public bool IsRootState { get => _isRootState; set => _isRootState = value; }
    public PlayerBaseState CurrentSubState { get => _currentSubState; set => _currentSubState = value; }
    public PlayerBaseState CurrentSuperState { get => _currentSuperState; set => _currentSuperState = value; }
    public PlayerBaseState PrevState { get => _prevState; set => _prevState = value; }
    public Player Player { get => _player; set => _player = value; }

    public PlayerBaseState(PlayerStateMachine currentContext, string animBoolName) 
    {

        _ctx = currentContext;
        _animBoolName = animBoolName;
        _core = currentContext.Core;
        Player = currentContext.Player;
    }

    public virtual void EnterState()
    {
        DoChecks();
        InitializeSubstate();
        Player.Anim.SetBool(_animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void ExitState()
    {
        Player.Anim.SetBool(_animBoolName, false);
        isExitingState = true;
    }
    public abstract void InitializeSubstate();
    public virtual void DoChecks()
    {
        xInput = Player.InputHandler.NormInputX;
        yInput = Player.InputHandler.NormInputY;

        if (CollisionSenses)
        {
            isGrounded = Player.Ground.GetOnGround();
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
            isTouchingCeiling = CollisionSenses.Ceiling;
        }
    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

    public void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    public void SetSubState(PlayerBaseState newSubState)
    {
        CurrentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

    public void SetPrevState(PlayerBaseState prevState)
    {
        PrevState = prevState;
    }

    public void UpdateStates()
    {
        LogicUpdate();
        if (_currentSubState != null)
        {
            CurrentSubState.UpdateStates();
        }
    }

    public void FixedUpdateStates()
    {
        PhysicsUpdate();
        if (_currentSubState != null)
        {
            _currentSubState.FixedUpdateStates();
        }
    }

}
