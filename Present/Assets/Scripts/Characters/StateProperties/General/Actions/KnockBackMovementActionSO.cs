using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "KnockBackMovementVector", menuName = "State Machines/Actions/General/KnockBackVelo")]
public class KnockBackMovementActionSO : StateActionSO<KnockBackMovementAction>
{

}

public class KnockBackMovementAction : StateAction
{
    private Damageable _damageable;
    private Character _character;

    public override void Awake(StateMachine stateMachine)
    {
        _damageable = stateMachine.GetComponent<Damageable>();
        _character = stateMachine.GetComponent<Character>();
    }

    public override void OnStateEnter()
    {
        _character.movementVector.x = _damageable.lastHitData.AbilityParam.knockBackDirection.x * _damageable.lastHitData.AbilityParam.knockBackVelocity * (_character.transform.position.x - _damageable.lastHitData.Source.transform.position.x >= 0 ? 1 : -1);
        _character.movementVector.y = _damageable.lastHitData.AbilityParam.knockBackDirection.y * _damageable.lastHitData.AbilityParam.knockBackVelocity;
    }

    public override void OnUpdate()
    {

    }
}
