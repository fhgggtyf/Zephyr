using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private float timer = 0;

    public PlayerJumpState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory){
        _isRootState = true;
        InitializeSubstate();
    }

    public override void EnterState()
    {

    }   

    public override void UpdateState()
    {
        _ctx.Velocity = _ctx.Body.velocity;

        if (_ctx.DesiredJump)
        {
            _ctx.DesiredJump = false;
            JumpAction();
        }

        if (_ctx.VelocityY <= -11)
        {
            _ctx.Body.gravityScale = 0;
        }
        else
        {
            _ctx.Body.gravityScale = _ctx.DefaultGravityScale;
        }


        _ctx.Body.velocity = _ctx.Velocity;

        if (timer <= 0.2)
        {
            timer += Time.deltaTime;
        }
        else
        {
            CheckSwitchStates();
        }


    }

    public override void FixedUpdateState()
    {

    }

    public override void OnCollisionEnter()
    {
        throw new System.NotImplementedException();
    }

    private void JumpAction()
    {
        if ( _ctx.JumpPhase < _ctx.MaxJumps)
        {
            _ctx.JumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _ctx.JumpHeight);
            if (_ctx.Velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - _ctx.VelocityY, 0f);
            }

            _ctx.VelocityY = jumpSpeed;
        }
    }

    public override void ExitState()
    {
        _ctx.CanDash = true;
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.OnGround)
        {
            _ctx.JumpPhase = 0;
            SwitchState(_factory.Grounded());
        }

    }

    public override void InitializeSubstate()
    {
        if (_ctx.input.RetrieveMoveInput() == 0)
        {
            SetSubState(_factory.Idle());
        }
        else if (_ctx.input.RetrieveMoveInput() != 0)
        {
            if (_ctx.VelocityX >= 7 || _ctx.VelocityX <= -7)
            {
                SetSubState(_factory.Run());
            }
            else
            {
                SetSubState(_factory.Walk());
            }

        }
        else if (_ctx.input.RetrieveRollInput())
        {
            SetSubState(_factory.Dash());
        }
    }

}
