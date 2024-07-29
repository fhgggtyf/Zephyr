using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "DeactivateHorizontal", menuName = "State Machines/Actions/Deactivate Horizontal")]
public class DeactivateHorizontalVelocityActionSO : StateActionSO<DeactivateHorizontalVelocityAction>
{
	public float horizontalVelo = 0;
}

public class DeactivateHorizontalVelocityAction : StateAction
{
	//Component references
	private Player _player;

	private DeactivateHorizontalVelocityActionSO _originSO => (DeactivateHorizontalVelocityActionSO)base.OriginSO; // The SO this StateAction spawned from

	public override void Awake(StateMachine stateMachine)
	{
		_player = stateMachine.GetComponent<Player>();
	}

	public override void OnUpdate()
	{
		_player.movementVector.x = _originSO.horizontalVelo;
	}
}
