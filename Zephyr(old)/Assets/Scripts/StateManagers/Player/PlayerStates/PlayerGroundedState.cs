using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    private bool jumpInput;
    public PlayerGroundedState(PlayerStateMachine currentContext, string animBoolName) : base(currentContext, animBoolName)
    {
        IsRootState = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        jumpInput = Player.InputHandler.JumpInput;
        if (!isGrounded)
        {
            _ctx.SwitchState(this, _ctx.Factory.InAir());
        }
        if (jumpInput)
        {
            Player.capabilities[(int)Capability.jump].CapabilityAction();
            _ctx.SwitchState(this, _ctx.Factory.InAir());
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

    public override void InitializeSubstate()
    {
        DoChecks();

        if (xInput != 0)
        {
            SetSubState(_ctx.Factory.Walk());
        }
        else if (xInput == 0)
        {
            SetSubState(_ctx.Factory.Idle());
        }
    }


}
