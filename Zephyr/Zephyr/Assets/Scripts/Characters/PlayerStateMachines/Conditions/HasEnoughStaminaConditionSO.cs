using UnityEngine;
using UnityEngine.Events;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/ Dash Roll Has Enough Stamina")]
public class HasEnoughStaminaConditionSO : StateConditionSO
{
    public RollDashTimeStaminaCostActionSO referenceSO = default;

    protected override Condition CreateCondition() => new HasEnoughStaminaCondition(referenceSO);
}

public class HasEnoughStaminaCondition : Condition
{
    private readonly float _cost;
    private PlayerStatsManager _statsManager;
    private Player _player;

    public HasEnoughStaminaCondition(RollDashTimeStaminaCostActionSO reference)
    {
        _cost = reference.baseCost;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _statsManager = stateMachine.GetComponent<PlayerStatsManager>();
        _player = stateMachine.GetComponent<Player>();
    }

    protected override bool Statement()
    {
        bool result = _statsManager.GetCurrentStamina() >= _cost;

        _player.isRolling = result;

        return result;
    }
}
