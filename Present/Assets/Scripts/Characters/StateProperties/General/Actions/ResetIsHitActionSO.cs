using System.Collections;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using Moment = Zephyr.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(fileName = "ResetIsHitAction", menuName = "State Machines/Actions/General/ResetIsHit")]
public class ResetIsHitActionSO : StateActionSO<ResetIsHit>
{
    public Moment whenToRun = default;
}

public class ResetIsHit : StateAction
{
    private Damageable _damageable;
    private ResetIsHitActionSO _originSO => (ResetIsHitActionSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _damageable = stateMachine.GetComponent<Damageable>();
    }

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

    public void SetParameter()
    {
        _damageable.GetHit = false;
    }
    public override void OnUpdate()
    {
    }
}
