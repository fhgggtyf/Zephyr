using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Running")]
public class IsRunningConditionSO : StateConditionSO<IsRunningCondition>
{ }

public class IsRunningCondition : Condition
{
	private Player _playerScript;

	public override void Awake(StateMachine stateMachine)
	{
		_playerScript = stateMachine.GetComponent<Player>();
	}

	protected override bool Statement()
	{
		return _playerScript.isRunning;
	}
}