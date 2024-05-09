using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateFactory<TStateMachine, TBaseState>
    where TStateMachine : IStateMachine<TBaseState>
    where TBaseState : IBaseState
{
    protected TStateMachine _context;

    protected StateFactory(TStateMachine context)
    {
        _context = context;
    }
}
