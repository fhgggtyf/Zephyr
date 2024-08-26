using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "DeactivateGravity", menuName = "State Machines/Actions/Deactivate Gravity")]
public class DeactivateGravityActionSO : StateActionSO<DeactivateGravityAction>
{
	public float verticalPull = 0;
}

public class DeactivateGravityAction : StateAction
{
	//Component references
	private Player _player;

	private DeactivateGravityActionSO _originSO => (DeactivateGravityActionSO)base.OriginSO; // The SO this StateAction spawned from

	public override void Awake(StateMachine stateMachine)
	{
		_player = stateMachine.GetComponent<Player>();
	}

	public override void OnUpdate()
	{
		_player.movementVector.y = _originSO.verticalPull;
	}
}
