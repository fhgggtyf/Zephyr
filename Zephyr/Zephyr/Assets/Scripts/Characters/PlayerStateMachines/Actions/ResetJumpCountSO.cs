using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using Moment = Zephyr.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(fileName = "ResetJumpCountAction", menuName = "State Machines/Actions/Reset Jumps")]
public class ResetJumpCountSO : StateActionSO<ResetJumpCount>
{
    public Moment whenToRun = default;
}

public class ResetJumpCount : StateAction
{
    private Player _player;
    private ResetJumpCountSO _originSO => (ResetJumpCountSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnUpdate() { }

    public override void OnStateEnter()
    {
        if (_originSO.whenToRun == SpecificMoment.OnStateEnter)
            SetParameter();
    }

    public override void OnStateExit()
    {
        if (_originSO.whenToRun == SpecificMoment.OnStateExit)
            SetParameter();
    }

    private void SetParameter()
    {
        _player.jumpCount = 0;
        _player.jumpIncremented = false;
    }
}
