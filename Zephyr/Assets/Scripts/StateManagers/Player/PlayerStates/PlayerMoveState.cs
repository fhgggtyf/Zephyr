using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    Vector2 desiredVelocity;
    float currentVelocityX;
    float maxSpeedChange;
    public PlayerMoveState(Player currentContext, string animBoolName) : base(currentContext, animBoolName)
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
        Debug.Log("LOL");

        DoChecks();

        Movement?.CheckIfShouldFlip(xInput);



        if (!isExitingState)
        {
            if (xInput == 0)
            {
                StateMachine.SwitchState(player.stateFactory.Idle());
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        desiredVelocity = new Vector2(Movement.FacingDirection, 0f) * Mathf.Max(player.PlayerData.MaxSpeed - player.Ground.GetFriction(), 0f);
        currentVelocityX = player.RB.velocity.x;
        maxSpeedChange = player.PlayerData.acceleration * Time.deltaTime;
        currentVelocityX = Mathf.MoveTowards(currentVelocityX, desiredVelocity.x, maxSpeedChange);

        Movement.SetVelocityX(currentVelocityX);
    }
}
