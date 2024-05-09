using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory : StateFactory<PlayerStateMachine, PlayerBaseState>
{
    public PlayerStateFactory(PlayerStateMachine context) : base(context)
    {
    }

    public PlayerBaseState InAir()
    {
        return new PlayerInAirState(_context, "InAir");
    }

    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, "Grounded");
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, "Idle");
    }

    public PlayerBaseState Walk()
    {
        return new PlayerMoveState(_context, "Move");
    }

    //public PlayerBaseState Run()
    //{
    //    return new PlayerRunState(_context);
    //}

    //public PlayerBaseState Shift()
    //{
    //    return new PlayerShiftState(_context);
    //}

    //public PlayerBaseState RunShift()
    //{
    //    return new PlayerRunShiftState(_context);
    //}

    //public PlayerBaseState Crouch()
    //{
    //    return new PlayerCrouchState(_context);
    //}
    //public PlayerBaseState Climb()
    //{
    //    return new PlayerClimbState(_context);
    //}
    //public PlayerBaseState Attack()
    //{
    //    return new PlayerAttackState(_context);
    //}


}
