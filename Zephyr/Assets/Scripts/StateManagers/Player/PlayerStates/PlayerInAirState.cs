using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerBaseState
{
    private int xInput;
    private bool jumpInput; 
    private bool dashInput;

    private int jumpCount = 0;
    private bool canDash = true;

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

    //Checks
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool isTouchingCeiling;


    public PlayerInAirState(Player currentContext, string animBoolName, bool hasJumped) : base(currentContext, animBoolName)
    {
        InitializeSubstate();
        IsRootState = true;
        if (hasJumped)
        {
            jumpCount++;
        }
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

    public override void LogicUpdate()
    {
        DoChecks();

        if (isGrounded)
        {
            jumpCount = 0;
            canDash = true;
            StateMachine.SwitchState(player.stateFactory.Grounded());
        }

        if (jumpInput && jumpCount < player.PlayerData.maxAirJumps)
        {
            jumpCount++;
            player.capabilities[(int)Capability.jump].CapabilityAction();
        }

        if (dashInput && canDash)
        {
            canDash = false;
            player.capabilities[(int)Capability.dash].CapabilityAction();
        }

    }

    public override void PhysicsUpdate()
    {
    }


}
