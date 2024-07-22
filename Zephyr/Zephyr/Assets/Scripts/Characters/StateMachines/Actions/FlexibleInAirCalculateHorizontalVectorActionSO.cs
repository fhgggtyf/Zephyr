using System;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "FlexibleInAirCalculateHorizontalVectorAction", menuName = "State Machines/Actions/FlexibleInAirCalculateHorizontalVector")]
public class FlexibleInAirCalculateHorizontalVectorActionSO : StateActionSO<FlexibleInAirCalculateHorizontalVectorAction>
{
    [Tooltip("Horizontal X plane speed multiplier")]
    public float speed = 4f;
}
public class FlexibleInAirCalculateHorizontalVectorAction : StateAction
{
    private Player _player;

    private float _runMultiplier;
    private FlexibleInAirCalculateHorizontalVectorActionSO _originSO => (FlexibleInAirCalculateHorizontalVectorActionSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnStateEnter()
    {
        switch (_player.isRunning)
        {
            case false:
                _runMultiplier = 1;
                break;
            case true:
                _runMultiplier = 1.6f;
                break;
        }
    }
    public override void OnUpdate()
    {
        _player.movementVector.x = _player.InputVector.x * _originSO.speed * _runMultiplier;
    }
}
