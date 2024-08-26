using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Grounded")]
public class IsGroundedConditionSO : StateConditionSO<IsGroundedCondition>
{
}

public class IsGroundedCondition : Condition
{
	private Player _playerScript;
	private CollisionSenses _collisionSenses;
	public override void Awake(StateMachine stateMachine)
	{
		_playerScript = stateMachine.GetComponent<Player>();
		_collisionSenses = _playerScript.Core.GetCoreComponent(ref _collisionSenses);
	}

	protected override bool Statement()
	{
		return _collisionSenses.Ground;
	}
}
