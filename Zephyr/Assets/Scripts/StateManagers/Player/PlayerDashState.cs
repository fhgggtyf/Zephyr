using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private float timer = 0f;
    private float dashVelocity;

    public PlayerDashState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory){ }

    public override void EnterState()
    {
        _ctx.DirectionX = _ctx.input.RetrieveMoveInput();
        DashAction();
    }
    
    public override void UpdateState()
    {
        _ctx.Velocity = _ctx.Body.velocity;
        _ctx.VelocityX -= _ctx.IsFacingRight * _ctx.DashDecceleration * Time.deltaTime;
        _ctx.VelocityY = 0f;
        _ctx.Body.velocity = _ctx.Velocity;
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (timer < Mathf.Abs(dashVelocity) / _ctx.DashDecceleration)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (_ctx.input.RetrieveMoveInput() == 0)
            {
                SwitchState(_factory.Idle());
            }
            if (_ctx.input.RetrieveMoveInput() != 0)
            {
                SwitchState(_factory.Walk());
            }
        }
    }

    public override void ExitState()
    {

    }

    public override void FixedUpdateState()
    {

    }

    public override void InitializeSubstate()
    {

    }

    public override void OnCollisionEnter()
    {

    }

    private void DashAction()
    {
        _ctx.Velocity = _ctx.Body.velocity;
        dashVelocity = _ctx.IsFacingRight * Mathf.Sqrt(2f * _ctx.DashDecceleration * _ctx.DashDesiredDistance);
        _ctx.VelocityX = dashVelocity;
        _ctx.Body.velocity = _ctx.Velocity;
    }
}
