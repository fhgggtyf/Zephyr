using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Height can Climb")]
public class HeightCanClimbConditionSO : StateConditionSO<HeightCanClimbCondition>
{
}

public class HeightCanClimbCondition : Condition
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
		return _collisionSenses.ClimbableFront;
	}
}
