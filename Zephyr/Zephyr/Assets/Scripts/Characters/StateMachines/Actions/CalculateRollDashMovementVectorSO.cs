using System;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HorizontalRoll/Dash", menuName = "State Machines/Actions/Horizontal Roll Dash")]
public class CalculateRollDashMovementVectorSO : StateActionSO<CalculateRollDashMovementVector>
{
    [Tooltip("Horizontal X plane speed multiplier")]
    public float speed = 4f;

    public FlashState flashState;

    [NonSerialized] public float RollDashMultiplier; 
}
public class CalculateRollDashMovementVector : StateAction
{

    private Player _player;

    protected Movement Movement
    {
        get => movement ?? _player.Core.GetCoreComponent(ref movement);
    }

    private Movement movement;
    private CalculateRollDashMovementVectorSO _originSO => (CalculateRollDashMovementVectorSO)base.OriginSO; // The SO this StateAction spawned from
    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnStateEnter()
    {
        switch (_originSO.flashState)
        {
            case FlashState.Roll:
                _originSO.RollDashMultiplier = 2;
                break;
            case FlashState.Dash:
                _originSO.RollDashMultiplier = 7.5f;
                break;
        }
    }
    public override void OnUpdate()
    {
        _player.movementVector.x = Movement.FacingDirection * _originSO.speed * _originSO.RollDashMultiplier;
    }
}

public enum FlashState
{
    Roll,
    Dash
}
