using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Can Attack")]
public class CanAttackConditionSO : StateConditionSO
{
    public CombatInputs weaponIndex;
    protected override Condition CreateCondition() => new CanAttackCondition(weaponIndex);

}
public class CanAttackCondition : Condition
{
    private CombatInputs _weaponIndex;

    private Player _player;
    private Weapon _weapon;

    public CanAttackCondition(CombatInputs weaponIndex)
    {
        _weaponIndex = weaponIndex;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();

    }

    public override void OnStateEnter()
    {
        _weapon = _player.weapons[(int)_weaponIndex];
    }

    protected override bool Statement()
    {
        return _weapon.CanEnterAttack;
    }
}
