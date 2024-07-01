using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "JumpBoost", menuName = "State Machines/Actions/JumpBoost")]
public class JumpBoostActionSO : StateActionSO<JumpBoostAction>
{
	public float initVelo = 10f;
}

public class JumpBoostAction : StateAction
{
	//Component references
	private Player _player;

    private JumpBoostActionSO _originSO => (JumpBoostActionSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnStateEnter()
    {
        _player.movementVector.y = _originSO.initVelo;
    }

    public override void OnUpdate() { }
}
