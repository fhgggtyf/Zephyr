using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Started ClimbMoving")]
public class IsClimbMovingConditionSO : StateConditionSO<IsClimbMovingCondition>
{
	public float treshold = 0.02f;
}

public class IsClimbMovingCondition : Condition
{
	private Player _playerScript;
	private IsClimbMovingConditionSO _originSO => (IsClimbMovingConditionSO)base.OriginSO; // The SO this Condition spawned from

	public override void Awake(StateMachine stateMachine)
	{
		_playerScript = stateMachine.GetComponent<Player>();
	}

	protected override bool Statement()
	{
		Vector2 movementVector = _playerScript.InputVector;
		movementVector.x = 0f;
		return movementVector.sqrMagnitude > _originSO.treshold;
	}
}
