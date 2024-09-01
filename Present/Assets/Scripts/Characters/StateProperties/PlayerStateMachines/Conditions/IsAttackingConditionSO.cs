using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/IsAttacking")]
public class IsAttackingConditionSO : StateConditionSO<IsAttackingCondition>
{
	public CombatInputs combatInput;
}
public class IsAttackingCondition : Condition
{
    private Player _playerScript;
	private IsAttackingConditionSO _originSO => (IsAttackingConditionSO)base.OriginSO; // The SO this Condition spawned from

	public override void Awake(StateMachine stateMachine)
	{
		_playerScript = stateMachine.GetComponent<Player>();
	}

	protected override bool Statement()
	{
		return _playerScript.attackInput[(int)_originSO.combatInput];
	}
}
