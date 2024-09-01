using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Climbing")]
public class IsClimbingConditionSO : StateConditionSO<IsClimbingCondition>
{
}

public class IsClimbingCondition : Condition
{
	private Player _playerScript;

	public override void Awake(StateMachine stateMachine)
	{
		_playerScript = stateMachine.GetComponent<Player>();
	}

	protected override bool Statement()
	{
		return _playerScript.isClimbing;
	}
}
