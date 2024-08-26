using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "KnockBackMovementVector", menuName = "State Machines/Actions/KnockBackVelo")]
public class KnockBackMovementActionSO : StateActionSO<KnockBackMovementAction>
{

}

public class KnockBackMovementAction : StateAction
{
    private Damageable _damageable;
    private Player _player;

    public override void Awake(StateMachine stateMachine)
    {
        _damageable = stateMachine.GetComponent<Damageable>();
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnStateEnter()
    {
        _player.movementVector.x = _damageable.lastHitData.AbilityParam.knockBackDirection.x * _damageable.lastHitData.AbilityParam.knockBackVelocity * (_player.transform.position.x - _damageable.lastHitData.Source.transform.position.x >= 0 ? 1 : -1);
        _player.movementVector.y = _damageable.lastHitData.AbilityParam.knockBackDirection.y * _damageable.lastHitData.AbilityParam.knockBackVelocity;
    }

    public override void OnUpdate()
    {

    }
}
