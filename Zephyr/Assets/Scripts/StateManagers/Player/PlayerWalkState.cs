using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{

    public PlayerWalkState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {


    }
    public override void UpdateState()
    {   

        _ctx.DirectionX = _ctx.input.RetrieveMoveInput();

        _ctx.DesiredVelocity = new Vector2(_ctx.DirectionX, 0f) * Mathf.Max(_ctx.MaxSpeed - _ctx.Ground.GetFriction(), 0f);
        _ctx.Velocity = _ctx.Body.velocity;
        _ctx.MaxSpeedChange = _ctx.Acceleration * Time.deltaTime;
        _ctx.VelocityX = Mathf.MoveTowards(_ctx.VelocityX, _ctx.DesiredVelocityX, _ctx.MaxSpeedChange);

        _ctx.Body.velocity = _ctx.Velocity;
        if (_ctx.DirectionX > 0)
        {
            _ctx.IsFacingRight = 1;
        }
        else if (_ctx.DirectionX < 0)
        {
            _ctx.IsFacingRight = -1;
        }
        
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {

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
        if (_ctx.input.RetrieveMoveInput() == 0)
        {
            SwitchState(_factory.Idle());
        }
        if (_ctx.input.RetrieveRollInput() && _currentSuperState is PlayerGroundedState)
        {
            SwitchState(_factory.Roll());
        }
        else if (_ctx.input.RetrieveRollInput() && _currentSuperState is PlayerJumpState)
        {
            SwitchState(_factory.Dash());
        }
    }

    public override void InitializeSubstate()
    {
        throw new System.NotImplementedException();
    }

}
