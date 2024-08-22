using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "InitVeritcalVelo", menuName = "State Machines/Actions/InitVeritcalVelo")]
public class InitVerticalVeloActionSO : StateActionSO<InitVeritcalVeloAction>
{
	public float initVelo = 10f;
}

public class InitVeritcalVeloAction : StateAction
{
	//Component references
	private Player _player;

    private InitVerticalVeloActionSO _originSO => (InitVerticalVeloActionSO)base.OriginSO; // The SO this StateAction spawned from

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
