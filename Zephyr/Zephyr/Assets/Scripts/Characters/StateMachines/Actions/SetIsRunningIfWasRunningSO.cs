using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetIsRunningIfWasRunningAction", menuName = "State Machines/Actions/check if prev running")]
public class SetIsRunningIfWasRunningSO : StateActionSO
{
    public bool isRunningParam = default;
    protected override StateAction CreateAction() => new SetIsRunningIfWasRunning(isRunningParam);

}
public class SetIsRunningIfWasRunning : StateAction
{
    private Player _playerScript;
    private StateMachine _stateMachine;
    private SetIsRunningIfWasRunningSO _originSO => (SetIsRunningIfWasRunningSO)base.OriginSO; // The SO this StateAction spawned from

    private bool isRunningParam;
    private StateTag stateBefore;

    public SetIsRunningIfWasRunning(bool isRunningParam)
    {
        this.isRunningParam = isRunningParam;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _playerScript = stateMachine.GetComponent<Player>();
        _stateMachine = stateMachine;
    }

    public override void OnStateEnter()
    {

        stateBefore = _stateMachine.GetPreviousState().stateTag;
        if (stateBefore == StateTag.Run)
        {
            SetParameter();
        }
    }

    private void SetParameter()
    {
        _playerScript.isRunning = isRunningParam;
    }

    public override void OnUpdate() { }

}
