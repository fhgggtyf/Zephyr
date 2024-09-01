using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/General/Is Attack Finished")]
public class IsAttackFinishedConditionSO : StateConditionSO<IsAttackFinishedCondition>
{
}

public class IsAttackFinishedCondition : Condition
{
    private Character _character;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    protected override bool Statement()
    {
        return _character.isAttackFinished;
    }
}