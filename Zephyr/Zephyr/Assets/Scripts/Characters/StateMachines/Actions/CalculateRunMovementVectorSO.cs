using System;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HorizontalRun", menuName = "State Machines/Actions/Horizontal Run")]
public class CalculateRunMovementVectorSO : StateActionSO<CalculateRunMovementVector>
{
    [Tooltip("Horizontal X plane speed multiplier")]
    public float speed = 4f;

    public MoveState moveState;

    [NonSerialized] public float runMultiplier; 
}
public class CalculateRunMovementVector : StateAction
{

    private Player _player;
    private CalculateRunMovementVectorSO _originSO => (CalculateRunMovementVectorSO)base.OriginSO; // The SO this StateAction spawned from
    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnStateEnter()
    {
        switch (_originSO.moveState)
        {
            case MoveState.Walk:
                _originSO.runMultiplier = 1;
                break;
            case MoveState.Run:
                _originSO.runMultiplier = 1.6f;
                break;
            case MoveState.Crouch:
                _originSO.runMultiplier = 0.6f;
                break;
        }
    }
    public override void OnUpdate()
    {
        _player.movementVector.x = _player.InputVector.x * _originSO.speed * _originSO.runMultiplier;
    }
}

public enum MoveState
{
    Walk,
    Run,
    Crouch
}
