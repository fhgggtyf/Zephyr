using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using Moment = Zephyr.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(fileName = "IsRunningParameterAction", menuName = "State Machines/Actions/Set running Parameter")]
public class IsRunningParameterActionSO : StateActionSO
{
    public bool isRunningParam = default;

    public Moment whenToRun = default;
    protected override StateAction CreateAction() => new IsRunningParameterAction(isRunningParam);

}
public class IsRunningParameterAction : StateAction
{
    private Player _playerScript;

    private IsRunningParameterActionSO _originSO => (IsRunningParameterActionSO)base.OriginSO; // The SO this StateAction spawned from

    private bool isRunningParam;

    public IsRunningParameterAction(bool isRunningParam)
    {
        this.isRunningParam = isRunningParam;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _playerScript = stateMachine.GetComponent<Player>();
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

    private void SetParameter()
    {
        _playerScript.isRunning = isRunningParam;
    }

    public override void OnUpdate() { }

}
