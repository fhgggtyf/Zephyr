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
            SetSubState(_factory.Walk());
        }
        else if (_ctx.input.RetrieveRollInput())
        {
            SetSubState(_factory.Roll());
        }
    }

    public override void OnCollisionEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.DesiredJump)
        {
            SwitchState(_factory.Jump());
        }
    }


}
