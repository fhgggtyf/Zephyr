using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    Vector2 desiredVelocity;
    float currentVelocityX;
    float maxSpeedChange;
    public PlayerMoveState(PlayerStateMachine currentContext, string animBoolName) : base(currentContext, animBoolName)
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

        Movement?.CheckIfShouldFlip(xInput);



        if (!isExitingState)
        {
            if (xInput == 0)
            {
                _ctx.SwitchState(this, _ctx.Factory.Idle());
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        desiredVelocity = new Vector2(Movement.FacingDirection, 0f) * Mathf.Max(Player.PlayerData.MaxSpeed - Player.Ground.GetFriction(), 0f);
        currentVelocityX = Player.RB.velocity.x;
        maxSpeedChange = Player.PlayerData.acceleration * Time.deltaTime;
        currentVelocityX = Mathf.MoveTowards(currentVelocityX, desiredVelocity.x, maxSpeedChange);

        Movement.SetVelocityX(currentVelocityX);
    }
}
