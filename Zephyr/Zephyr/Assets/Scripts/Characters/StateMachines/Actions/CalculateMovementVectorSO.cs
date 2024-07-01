using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HorizontalMove", menuName = "State Machines/Actions/Horizontal Move")]
public class CalculateMovementVectorSO : StateActionSO<CalculateMovementVector>
{
    [Tooltip("Horizontal X plane speed multiplier")]
    public float speed = 4f;
}
public class CalculateMovementVector : StateAction
{

    private Player _player;
    private CalculateMovementVectorSO _originSO => (CalculateMovementVectorSO)base.OriginSO; // The SO this StateAction spawned from
    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }
    public override void OnUpdate()
    {
        _player.movementVector.x = _player.InputVector.x * _originSO.speed;
    }
}
