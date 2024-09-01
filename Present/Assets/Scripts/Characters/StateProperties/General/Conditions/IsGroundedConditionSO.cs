using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/General/Is Grounded")]
public class IsGroundedConditionSO : StateConditionSO<IsGroundedCondition>
{
}

public class IsGroundedCondition : Condition
{
    private Character _character;
    private CollisionSenses _collisionSenses;
    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
        _collisionSenses = _character.Core.GetCoreComponent(ref _collisionSenses);
    }

    protected override bool Statement()
    {
        return _collisionSenses.Ground;
    }
}
