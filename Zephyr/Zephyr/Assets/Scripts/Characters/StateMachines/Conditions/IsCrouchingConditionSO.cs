using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Crouching")]
public class IsCrouchingConditionSO : StateConditionSO<IsCrouchingCondition>
{ }

public class IsCrouchingCondition : Condition
{
	private Player _playerScript;

	public override void Awake(StateMachine stateMachine)
	{
		_playerScript = stateMachine.GetComponent<Player>();
	}

	protected override bool Statement()
	{
		return _playerScript.isCrouching;
	}
}