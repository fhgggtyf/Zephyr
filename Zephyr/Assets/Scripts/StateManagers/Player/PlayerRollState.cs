using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    private float timer = 0f;
    private float rollVelocity;

    public PlayerRollState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        _ctx.DirectionX = _ctx.input.RetrieveMoveInput();
        RollAction(_ctx.DirectionX);
    }

    public override void UpdateState()
    {
        _ctx.Velocity = _ctx.Body.velocity;
        _ctx.VelocityX -= SignWithZero(_ctx.DirectionX) * _ctx.RollDecceleration * Time.deltaTime;
        _ctx.Body.velocity = _ctx.Velocity;
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (rollVelocity != 0 && timer < Mathf.Abs(rollVelocity) / _ctx.RollDecceleration)
        {
            timer += Time.deltaTime;
        }
        else if(rollVelocity == 0 && timer < 0.3f)
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
                if (_prevState is PlayerRunState)
                {
                    SwitchState(_factory.Run());
                }
                else if (_prevState is PlayerWalkState)
                {
                    SwitchState(_factory.Walk());
                }

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
        throw new System.NotImplementedException();
    }

    private void RollAction(float direction)
    {
        _ctx.Velocity = _ctx.Body.velocity;
        rollVelocity = SignWithZero(direction) * Mathf.Sqrt(2f * _ctx.RollDecceleration * _ctx.RollDesiredDistance);
        _ctx.VelocityX += rollVelocity;
        _ctx.Body.velocity = _ctx.Velocity;
    }

    int SignWithZero(float value)
    {
        if (value > 0f) return 1;
        if (value < 0f) return -1;
        return 0;
    }
}
