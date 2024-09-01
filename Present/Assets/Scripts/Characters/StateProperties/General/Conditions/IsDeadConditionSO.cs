using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/General/Is dead")]
public class IsDeadConditionSO : StateConditionSO<IsDeadCondition>
{
}

public class IsDeadCondition : Condition
{
    private Damageable _damageable;

    public override void Awake(StateMachine stateMachine)
    {
        _damageable = stateMachine.GetComponent<Damageable>();
    }

    protected override bool Statement()
    {
        return _damageable.IsDead;
    }
}