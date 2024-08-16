using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Stamina empty")]
public class IsStaminaEmptyConditionSO : StateConditionSO<IsStaminaEmpty>
{}

public class IsStaminaEmpty: Condition
{
    private PlayerStatsManager _statsManager;

    public override void Awake(StateMachine stateMachine)
    {
        _statsManager = stateMachine.GetComponent<PlayerStatsManager>();
    }

    protected override bool Statement()
    {
        return _statsManager.GetCurrentStamina() < 1;
    }
}
