using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Can Stand")]
public class CanStandUpConditionSO : StateConditionSO<CanStandUpCondition>
{
}

public class CanStandUpCondition : Condition
{
	private Player _playerScript;
	private CollisionSenses _collisionSenses;
	public override void Awake(StateMachine stateMachine)
	{
		_playerScript = stateMachine.GetComponent<Player>();
		_collisionSenses = _playerScript.Core.GetCoreComponent(ref _collisionSenses);
	}

    public override void OnStateExit()
    {
		Debug.Log(!_collisionSenses.CrouchCeiling);
    }

    protected override bool Statement()
	{
		return !_collisionSenses.CrouchCeiling;
	}
}
