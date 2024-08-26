using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Ceiling")]
public class IsCeilingConditionSO : StateConditionSO<IsCeilingCondition>
{
}

public class IsCeilingCondition : Condition
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
		return _collisionSenses.Ceiling;
	}
}
