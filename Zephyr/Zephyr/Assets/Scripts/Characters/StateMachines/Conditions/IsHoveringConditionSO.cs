using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Hovering")]
public class IsHoveringConditionSO : StateConditionSO<IsHoveringCondition>
{
	public float descendingThreashold = -2;
	public float ascendingThreshold = 2;
}

public class IsHoveringCondition : Condition
{
	private Player _playerScript;
	private IsHoveringConditionSO _originSO => (IsHoveringConditionSO)base.OriginSO; // The SO this Condition spawned from
	public override void Awake(StateMachine stateMachine)
	{
		_playerScript = stateMachine.GetComponent<Player>();
	}

	protected override bool Statement()
	{
		return _playerScript.movementVector.y <= _originSO.ascendingThreshold && _playerScript.movementVector.y >= _originSO.descendingThreashold;
	}
}
