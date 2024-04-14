using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    Player _ctx;
    PlayerBaseState _currentSubState;
    PlayerBaseState _currentSuperState;
    PlayerBaseState _prevState;

    public PlayerBaseState CurrentState { get; private set; }
    public Player Ctx { get => _ctx; set => _ctx = value; }
    public PlayerBaseState CurrentSubState { get => _currentSubState; set => _currentSubState = value; }
    public PlayerBaseState CurrentSuperState { get => _currentSuperState; set => _currentSuperState = value; }
    public PlayerBaseState PrevSubState { get => _prevState; set => _prevState = value; }


    public void SetCurrentState()
    {
        CurrentState = null;
        Debug.Log(CurrentState);
    }


    public void SetCurrentState(PlayerBaseState thisState)
    {
        CurrentState = thisState;
        if (CurrentState != null)
        {
            CurrentState.EnterState();
        }

        Debug.Log(CurrentState);
    }


    public void SwitchState(PlayerBaseState newState)
    {
        CurrentState.ExitState();

        newState.EnterState();

        if (CurrentSuperState != null)
        {
            CurrentSuperState.StateMachine.SetPrevSubState(CurrentState);
            CurrentSuperState.StateMachine.SetSubState(newState);
        }
        else
        {
            Ctx.StateMachine.SetPrevSubState(CurrentState);
            Ctx.StateMachine.SetSubState(newState);
        }

    }

    public void UpdateStates()
    {
        if (CurrentState != null)
        {
            CurrentState.LogicUpdate();
        }
        if (CurrentSubState != null)
        {
            CurrentSubState.StateMachine.UpdateStates();
        }
    }

    public void FixedUpdateStates()
    {
        if (CurrentState != null)
        {
            CurrentState.PhysicsUpdate();
        }
        if (CurrentSubState != null)
        {
            CurrentSubState.StateMachine.FixedUpdateStates();
        }
    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    public void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.StateMachine.SetSuperState(CurrentState);
    }

    protected void SetPrevSubState(PlayerBaseState prevState)
    {
        _prevState = prevState;
    }

}
