using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Enemies/AttackFinished")]
public class GeneralEnemyAttackFinishedConditionSO : StateConditionSO<GeneralEnemyAttackFinishedCondition>
{
}

public class GeneralEnemyAttackFinishedCondition : Condition
{
	private NonPlayerCharacter _npcScript;

	public override void Awake(StateMachine stateMachine)
	{
		_npcScript = stateMachine.GetComponent<NonPlayerCharacter>();
	}

	protected override bool Statement()
	{
		return _npcScript.attackFinished;
	}
}