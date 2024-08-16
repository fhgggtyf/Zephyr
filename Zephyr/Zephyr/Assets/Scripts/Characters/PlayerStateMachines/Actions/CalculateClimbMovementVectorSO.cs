using System;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "VerticalClimb", menuName = "State Machines/Actions/Vertical Climb")]
public class CalculateClimbMovementVectorSO : StateActionSO<CalculateClimbMovementVector>
{
    [Tooltip("Y plane speed multiplier")]
    public float speed = 4f;
}
public class CalculateClimbMovementVector : StateAction
{

    private Player _player;

    private CalculateClimbMovementVectorSO _originSO => (CalculateClimbMovementVectorSO)base.OriginSO; // The SO this StateAction spawned from
    
    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnUpdate()
    {
        _player.movementVector.y = _player.InputVector.y * _originSO.speed;
    }
}
