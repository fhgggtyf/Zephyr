using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is AbilityFinished")]
public class IsAbilityFinishedConditionSO : StateConditionSO<IsAbilityFinishedCondition>
{
}

public class IsAbilityFinishedCondition : Condition
{
	private Player _playerScript;

	public override void Awake(StateMachine stateMachine)
	{
		_playerScript = stateMachine.GetComponent<Player>();
	}

	protected override bool Statement()
	{
		return _playerScript.isAbilityFinished;
	}
}