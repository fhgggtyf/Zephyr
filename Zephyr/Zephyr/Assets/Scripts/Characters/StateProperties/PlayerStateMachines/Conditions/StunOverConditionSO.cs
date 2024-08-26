using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/StunOver")]
public class StunOverConditionSO : StateConditionSO<StunOverCondition>
{
}

public class StunOverCondition : Condition
{
    private Player _playerScript;

    public override void Awake(StateMachine stateMachine)
    {
        _playerScript = stateMachine.GetComponent<Player>();
    }

    protected override bool Statement()
    {
        return _playerScript.stunOver;
    }
}