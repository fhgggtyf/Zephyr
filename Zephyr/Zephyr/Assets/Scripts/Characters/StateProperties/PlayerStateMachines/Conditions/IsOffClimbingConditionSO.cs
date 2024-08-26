using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is OffClimbing")]
public class IsOffClimbingConditionSO : StateConditionSO<IsOffClimbingCondition>
{
}

public class IsOffClimbingCondition : Condition
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
		return _collisionSenses.OffClimb;
	}
}
