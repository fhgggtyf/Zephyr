using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Enemies/PlayerInAtkRange")]
public class GeneralEnemyPlayerInAttackRangeConditionSO : StateConditionSO<GeneralEnemyPlayerInAttackRangeCondition>
{
}

public class GeneralEnemyPlayerInAttackRangeCondition : Condition
{
	private NonPlayerCharacter _npcScript;

	public override void Awake(StateMachine stateMachine)
	{
		_npcScript = stateMachine.GetComponent<NonPlayerCharacter>();
	}

	protected override bool Statement()
	{
		return _npcScript.CheckPlayerInMinAgroRange();
	}
}