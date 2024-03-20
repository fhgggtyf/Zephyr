using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShiftState : PlayerBaseState
{
    public PlayerShiftState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) 
        : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {


    }
    public override void UpdateState()
    {
        _ctx.DirectionX = _ctx.input.RetrieveMoveInput();
        _ctx.DesiredVelocity = new Vector2(_ctx.DirectionX, 0f) * Mathf.Max(_ctx.MaxShiftSpeed - _ctx.Ground.GetFriction(), 0f);
        _ctx.Velocity = _ctx.Body.velocity;
        _ctx.MaxSpeedChange = _ctx.Acceleration * Time.deltaTime;
        _ctx.VelocityX = Mathf.MoveTowards(_ctx.VelocityX, _ctx.DesiredVelocityX, _ctx.MaxSpeedChange);

        _ctx.Body.velocity = _ctx.Velocity;

        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        AdjustDirection();
    }

    public override void OnCollisionEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (_ctx.input.RetrieveMoveInput() == 0 && !_ctx.input.RetrieveShiftInput())
        {
            SwitchState(_factory.Idle());
        }
        if (_ctx.input.RetrieveMoveInput() != 0 && !_ctx.input.RetrieveShiftInput())
        {
            SwitchState(_factory.Walk());
        }
        if (_ctx.input.RetrieveMoveInput() == 0 && _ctx.input.RetrieveShiftInput())
        {
            // crouch
        }
        if (_ctx.input.RetrieveRollInput())
        {
            SwitchState(_factory.Roll());
        }
    }

    public override void InitializeSubstate()
    {
        throw new System.NotImplementedException();
    }

    void AdjustDirection()
    {
        if (_ctx.input.RetrieveMoveInput() > 0 && _ctx.IsFacingRight != 1)
        {
            Turn();
        }
        else if (_ctx.input.RetrieveMoveInput() < 0 && _ctx.IsFacingRight != -1)
        {
            Turn();
        }
    }

    void Turn()
    {
        Vector3 rotator = new Vector3(_ctx.Transform.rotation.x, _ctx.IsFacingRight == 1 ? 180f : 0f, _ctx.Transform.rotation.z);
        _ctx.transform.rotation = Quaternion.Euler(rotator);
        _ctx.IsFacingRight = (short)-_ctx.IsFacingRight;
        _ctx.CamFollowed.CallTurn();
    }
}
