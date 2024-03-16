using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        _ctx.DesiredRoll |= _ctx.input.RetrieveRollInput();
        CheckSwitchStates();
        if (_currentSuperState is PlayerGroundedState)
        {
            _ctx.Velocity = new Vector2(_ctx.VelocityX * (1 - Time.deltaTime * _ctx.Acceleration), _ctx.VelocityY + Physics.gravity.y * _ctx.DefaultGravityScale * Time.deltaTime);
            _ctx.Body.velocity = _ctx.Velocity;
        }
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
        if (_ctx.input.RetrieveMoveInput() != 0)
        {
            SwitchState(_factory.Walk());
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
