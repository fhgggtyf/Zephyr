using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "InitVeritcalVelo", menuName = "State Machines/Actions/General/InitVeritcalVelo")]
public class InitVerticalVeloActionSO : StateActionSO<InitVeritcalVeloAction>
{
    public float initVelo = 10f;
}

public class InitVeritcalVeloAction : StateAction
{
    //Component references
    private Character _character;

    private InitVerticalVeloActionSO _originSO => (InitVerticalVeloActionSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Boost start");
        _character.movementVector.y = _originSO.initVelo;
    }

    public override void OnUpdate() { }

    public override void OnStateExit()
    {
        Debug.Log("Boost over");
    }
}
