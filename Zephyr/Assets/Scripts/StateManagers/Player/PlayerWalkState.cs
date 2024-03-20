using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{

    private float _timer = 0;

    public PlayerWalkState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {


    }
    public override void UpdateState()
    {
        _timer += Time.deltaTime;

        _ctx.DirectionX = _ctx.input.RetrieveMoveInput();
        _ctx.DesiredVelocity = new Vector2(_ctx.DirectionX, 0f) * Mathf.Max(_ctx.MaxSpeed - _ctx.Ground.GetFriction(), 0f);
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
         if (_ctx.input.RetrieveRunInput())
        {
            if (_timer <= 0.2f)
            {
                SwitchState(_factory.Run());
            }
            else
            {
                _timer = 0;
            }

        }
        if (_currentSuperState is PlayerGroundedState)
        {
            if (_ctx.input.RetrieveRollInput())
            {
                SwitchState(_factory.Roll());
            }
            if (_ctx.input.RetrieveShiftInput())
            {
                if (_ctx.input.RetrieveMoveInput() != 0)
                {
                    SwitchState(_factory.Shift());
                }
                // crouch
                else
                {
                    SwitchState(_factory.Crouch());
                }

            }
        }
        if (_currentSuperState is PlayerJumpState)
        {
            if (_ctx.input.RetrieveRollInput() && _ctx.CanDash)
            {
                SwitchState(_factory.Dash());
            }
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
