using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is hit")]
public class IsHitConditionSO : StateConditionSO<IsHitCondition>
{
}

public class IsHitCondition : Condition
{
    private Damageable _damageable;

    public override void Awake(StateMachine stateMachine)
    {
        _damageable = stateMachine.GetComponent<Damageable>();
    }

    protected override bool Statement()
    {
        return _damageable.GetHit;
    }
}