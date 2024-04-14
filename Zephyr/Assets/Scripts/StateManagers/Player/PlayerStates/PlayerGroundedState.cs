using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    protected int xInput;
    protected int yInput;

    private bool jumpInput;

    protected bool isTouchingCeiling;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;

    protected Movement Movement
    {
        get => movement ?? core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    private CollisionSenses CollisionSenses
    {
        get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
    }

    private CollisionSenses collisionSenses;

    public PlayerGroundedState(Player currentContext, string animBoolName) : base(currentContext, animBoolName)
    {
        IsRootState = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
            isTouchingCeiling = CollisionSenses.Ceiling;
        }
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        DoChecks();

        jumpInput = player.InputHandler.JumpInput;
        if (!isGrounded)
        {
            StateMachine.SwitchState(player.stateFactory.InAir(false));
        }
        if (jumpInput)
        {
            player.capabilities[(int)Capability.jump].CapabilityAction();
            StateMachine.SwitchState(player.stateFactory.InAir(true));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void InitializeSubstate()
    {
        xInput = player.InputHandler.NormInputX;

        //if (xInput != 0)
        //{
        //    StateMachine.SetSubState(player.stateFactory.Walk());
        //}
        //else if (xInput == 0)
        //{
        //    StateMachine.SetSubState(player.stateFactory.Idle());
        //}
    }


}
