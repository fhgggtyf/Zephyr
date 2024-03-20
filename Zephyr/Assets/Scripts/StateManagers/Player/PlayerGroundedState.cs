using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { 
        _isRootState = true;
        InitializeSubstate();
    }
    
    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
    }

    public override void FixedUpdateState()
    {

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
            SetSubState(_factory.Roll());
        }
        else if (_ctx.input.RetrieveShiftInput())
        {
            SetSubState(_factory.Shift());
        }

    }

    public override void OnCollisionEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.DesiredJump || !_ctx.OnGround)
        {
            SwitchState(_factory.Jump());
        }
    }


}
