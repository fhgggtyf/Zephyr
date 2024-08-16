using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "GroundGravity", menuName = "State Machines/Actions/Enemies/Ground Gravity")]
public class GeneralEnemyGroundGravityActionSO : StateActionSO<GeneralEnemyGroundGravityAction>
{
	public float verticalPull = -0.1f;
}

public class GeneralEnemyGroundGravityAction : StateAction
{
	//Component references
	private NonPlayerCharacter _npc;

	private GeneralEnemyGroundGravityActionSO _originSO => (GeneralEnemyGroundGravityActionSO)base.OriginSO; // The SO this StateAction spawned from

	public override void Awake(StateMachine stateMachine)
	{
		_npc = stateMachine.GetComponent<NonPlayerCharacter>();
	}

	public override void OnUpdate()
	{
		_npc.movementVector.y = _originSO.verticalPull;
	}
}
