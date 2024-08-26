using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "StopHorizontalMove", menuName = "State Machines/Actions/Stop Horizontal Move")]
public class StopHorizontalMovementVectorSO : StateActionSO<StopHorizontalMovementVector>
{
    [Tooltip("Horizontal X plane speed multiplier")]
    public float speed = 0f;
}
public class StopHorizontalMovementVector : StateAction
{
    private Player _player;
    private StopHorizontalMovementVectorSO _originSO => (StopHorizontalMovementVectorSO)base.OriginSO; // The SO this StateAction spawned from
    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }
    public override void OnUpdate()
    {
        _player.movementVector.x = _player.InputVector.x * _originSO.speed;
    }
}
