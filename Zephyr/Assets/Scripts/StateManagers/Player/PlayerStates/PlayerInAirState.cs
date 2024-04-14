using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerBaseState
{
    private bool jumpInput; 
    private bool dashInput;

    private int jumpCount = 1;
    private bool canDash = true;

    public PlayerInAirState(Player currentContext, string animBoolName) : base(currentContext, animBoolName)
    {
        IsRootState = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        jumpInput = player.InputHandler.JumpInput;

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

        if (xInput != 0)
        {
            StateMachine.SetSubState(player.stateFactory.Walk());
        }
        else if (xInput == 0)
        {
            StateMachine.SetSubState(player.stateFactory.Idle());
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
            StateMachine.SwitchState(player.stateFactory.Grounded());
        }

        if (jumpInput && jumpCount < player.PlayerData.MaxJumps)
        {
            jumpCount++;
            player.capabilities[(int)Capability.jump].CapabilityAction();
        }

        if (dashInput && canDash)
        {
            canDash = false;
            player.capabilities[(int)Capability.dash].CapabilityAction();
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


}
