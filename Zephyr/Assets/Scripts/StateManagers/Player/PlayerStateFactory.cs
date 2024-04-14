using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    Player _context;
    
    public PlayerStateFactory(Player currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState InAir(bool hasJumped)
    {
        return new PlayerInAirState(_context, "InAir", hasJumped);
    }

    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, "Grounded");
    }

    //public PlayerBaseState Idle()
    //{
    //    return new PlayerIdleState(_context);
    //}

    //public PlayerBaseState Walk()
    //{
    //    return new PlayerWalkState(_context);
    //}

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
