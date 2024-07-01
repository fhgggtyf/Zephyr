using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : IStateMachine<PlayerBaseState>
{
    PlayerStateFactory _factory;

    Core _core;

    PlayerData _data;

    Player _player;

    public PlayerBaseState CurrentState { get; private set; }
    public Core Core { get => _core; set => _core = value; }
    public Player Player { get => _player; set => _player = value; }
    public PlayerData Data { get => _data; set => _data = value; }
    public PlayerStateFactory Factory { get => _factory; set => _factory = value; }

    public PlayerStateMachine(Player player, PlayerData data, Core core)
    {
        Factory = new PlayerStateFactory(this);
        Data = data;
        Core = core;
        Player = player;

        SetCurrentState(Factory.Grounded());
        CurrentState.EnterState();
    }

    public void SetCurrentState(PlayerBaseState thisState)
    {
        CurrentState = thisState;
    }

    public void SwitchState(PlayerBaseState oldState, PlayerBaseState newState)
    {
        oldState.ExitState();

        newState.SetPrevState(oldState);

        if (!oldState.IsRootState)
        {
            oldState.CurrentSuperState.SetSubState(newState);
            newState.SetSuperState(oldState.CurrentSuperState);
        }
        else
        {
            SetCurrentState(newState);
        }

        newState.EnterState();

    }

    public void UpdateStates()
    {
        CurrentState.UpdateStates();
    }

    public void FixedUpdateStates()
    {
        CurrentState.FixedUpdateStates();
    }
}
