using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerBaseState
{
    private bool jumpInput; 
    private bool dashInput;

    private int jumpCount = 1;
    private bool canDash = true;

    public PlayerInAirState(PlayerStateMachine currentContext, string animBoolName) : base(currentContext, animBoolName)
    {
        IsRootState = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        jumpInput = Player.InputHandler.JumpInput;

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
        xInput = Player.InputHandler.NormInputX;

        if (xInput != 0)
        {
            SetSubState(_ctx.Factory.Walk());
        }
        else if (xInput == 0)
        {
            SetSubState(_ctx.Factory.Idle());
        }
    }

    public override void LogicUpdate()
    {
        DoChecks();

        Debug.Log(jumpCount + jumpInput.ToString());

        if (isGrounded)
        {
            jumpCount = 0;
            canDash = true;
            _ctx.SwitchState(this, _ctx.Factory.Grounded());
        }

        if (jumpInput && jumpCount < Player.PlayerData.MaxJumps)
        {
            jumpCount++;
            Player.capabilities[(int)Capability.jump].CapabilityAction();
        }

        if (dashInput && canDash)
        {
            canDash = false;
            Player.capabilities[(int)Capability.dash].CapabilityAction();
        }

        if (xInput != 0 && CurrentSubState is not PlayerMoveState)
        {
            SetSubState(_ctx.Factory.Walk());
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
