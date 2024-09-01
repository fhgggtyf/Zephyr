using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/General/StunOver")]
public class StunOverConditionSO : StateConditionSO<StunOverCondition>
{
}

public class StunOverCondition : Condition
{
    private Character _character;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    protected override bool Statement()
    {
        return _character.stunOver;
    }
}