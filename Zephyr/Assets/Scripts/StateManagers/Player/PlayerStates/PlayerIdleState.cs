using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{

    public PlayerIdleState(Player currentContext, string animBoolName) : base(currentContext, animBoolName)
    {
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


    public override void InitializeSubstate()
    {

    }

    public override void LogicUpdate()
    {
        DoChecks();

        if (!isExitingState)
        {
            if (xInput != 0)
            {
                StateMachine.SwitchState(player.stateFactory.Walk());
            }
        }
    }

    public override void PhysicsUpdate()
    {
        if (StateMachine.CurrentSuperState is PlayerGroundedState)
        {
            Movement.SetVelocityX(Movement.RB.velocity.x * (1 - Time.deltaTime * player.PlayerData.acceleration));
        }
    }
}
