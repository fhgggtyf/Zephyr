using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "DeactivateHorizontal", menuName = "State Machines/Actions/General/Deactivate Horizontal")]
public class DeactivateHorizontalVelocityActionSO : StateActionSO<DeactivateHorizontalVelocityAction>
{
	public float horizontalVelo = 0;
}

public class DeactivateHorizontalVelocityAction : StateAction
{
	//Component references
	private Character _character;

	private DeactivateHorizontalVelocityActionSO _originSO => (DeactivateHorizontalVelocityActionSO)base.OriginSO; // The SO this StateAction spawned from

	public override void Awake(StateMachine stateMachine)
	{
		_character = stateMachine.GetComponent<Character>();
	}

	public override void OnUpdate()
	{
		_character.movementVector.x = _originSO.horizontalVelo;
	}
}
