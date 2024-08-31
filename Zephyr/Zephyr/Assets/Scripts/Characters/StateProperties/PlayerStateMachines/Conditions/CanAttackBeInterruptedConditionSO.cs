using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Can Interupt Attack")]
public class CanAttackBeInterruptedConditionSO : StateConditionSO<CanAttackBeInterruptedCondition>
{
    public CombatInputs weaponIndex;
}
public class CanAttackBeInterruptedCondition : Condition
{
    private Player _player;

    private CanAttackBeInterruptedConditionSO _originSO => (CanAttackBeInterruptedConditionSO)base.OriginSO; // The SO this Condition spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    protected override bool Statement()
    {
        var currentweapon = _player.weapons[(int)_originSO.weaponIndex];
        return currentweapon.Data.CanBeInterupted[currentweapon.CurrentAttackCounter];
    }
}
