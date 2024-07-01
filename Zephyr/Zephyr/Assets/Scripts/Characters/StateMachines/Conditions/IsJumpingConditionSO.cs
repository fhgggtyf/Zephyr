using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Jumping")]
public class IsJumpingConditionSO : StateConditionSO<IsJumpingCondition>
{ }

public class IsJumpingCondition : Condition
{
	private Player _playerScript;

	public override void Awake(StateMachine stateMachine)
	{
		_playerScript = stateMachine.GetComponent<Player>();
	}

	protected override bool Statement()
	{
		return _playerScript.jumpInput;
	}
}
