using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    private bool jumpInput;
    public PlayerGroundedState(Player currentContext, string animBoolName) : base(currentContext, animBoolName)
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

        jumpInput = player.InputHandler.JumpInput;
        if (!isGrounded)
        {
            StateMachine.SwitchState(player.stateFactory.InAir());
        }
        if (jumpInput)
        {
            player.capabilities[(int)Capability.jump].CapabilityAction();
            StateMachine.SwitchState(player.stateFactory.InAir());
        }
        if (xInput != 0 && StateMachine.CurrentSubState is not PlayerMoveState)
        {
            StateMachine.SetSubState(player.stateFactory.Walk());
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
            StateMachine.SetSubState(player.stateFactory.Walk());
        }
        else if (xInput == 0)
        {
            StateMachine.SetSubState(player.stateFactory.Idle());
        }
    }


}
