using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetIsRunningIfWasRunningAction", menuName = "State Machines/Actions/check if prev running")]
public class SetIsRunningIfWasRunningSO : StateActionSO<SetIsRunningIfWasRunning>
{}
public class SetIsRunningIfWasRunning : StateAction
{
    private Player _playerScript;

    private StateMachine _stateMachine;

    private StateTag stateBefore;

    public override void Awake(StateMachine stateMachine)
    {
        _playerScript = stateMachine.GetComponent<Player>();
        _stateMachine = stateMachine;
    }

    public override void OnStateEnter()
    {

        stateBefore = _stateMachine.GetPreviousState().stateTag;
        if (stateBefore == StateTag.Run || _playerScript.isRunning)
        {
            SetParameter();
        }
    }

    private void SetParameter()
    {
        _playerScript.isRunning = true;
    }

    public override void OnUpdate() { }

}
