using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HorizontalRun", menuName = "State Machines/Actions/Horizontal Run")]
public class CalculateRunMovementVectorSO : StateActionSO<CalculateRunMovementVector>
{
    [Tooltip("Horizontal X plane speed multiplier")]
    public float speed = 4f;

    public float runMultiplier = 1.6f; 
}
public class CalculateRunMovementVector : StateAction
{

    private Player _player;
    private CalculateRunMovementVectorSO _originSO => (CalculateRunMovementVectorSO)base.OriginSO; // The SO this StateAction spawned from
    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }
    public override void OnUpdate()
    {
        _player.isRunning = true;
        _player.movementVector.x = _player.InputVector.x * _originSO.speed * _originSO.runMultiplier;
    }
}
