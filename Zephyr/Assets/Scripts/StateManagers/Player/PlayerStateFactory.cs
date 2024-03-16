using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    PlayerStateManager _context;
    
    public PlayerStateFactory(PlayerStateManager currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_context, this);
    }

    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }

    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(_context, this);
    }

    public PlayerBaseState Roll()
    {
        return new PlayerRollState(_context, this);
    }

    public PlayerBaseState Dash()
    {
        return new PlayerDashState(_context, this);
    }


}
