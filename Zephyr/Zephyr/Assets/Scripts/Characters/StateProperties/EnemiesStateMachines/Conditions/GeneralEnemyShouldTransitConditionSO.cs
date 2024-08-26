using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Enemies/Should Transit", order = 0)]
public class GeneralEnemyShouldTransitConditionSO : StateConditionSO<GeneralEnemyShouldTransitCondition>
{
}

public class GeneralEnemyShouldTransitCondition : Condition
{
	private NonPlayerCharacter _npcScript;

	public override void Awake(StateMachine stateMachine)
	{
		_npcScript = stateMachine.GetComponent<NonPlayerCharacter>();
	}

	protected override bool Statement()
	{
		return _npcScript.shouldTransit;
	}
}
