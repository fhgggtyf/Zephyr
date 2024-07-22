using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Stamina empty")]
public class IsStaminaEmptyConditionSO : StateConditionSO<IsStaminaEmpty>
{}

public class IsStaminaEmpty: Condition
{
    private StatsManager _statsManager;

    public override void Awake(StateMachine stateMachine)
    {
        _statsManager = stateMachine.GetComponent<StatsManager>();
    }

    protected override bool Statement()
    {
        return _statsManager.GetCurrentStamina() < 1;
    }
}
