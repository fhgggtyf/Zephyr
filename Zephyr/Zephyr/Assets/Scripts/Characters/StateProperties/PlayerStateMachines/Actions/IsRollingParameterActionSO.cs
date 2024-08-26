using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using Moment = Zephyr.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(fileName = "IsRollingParameterAction", menuName = "State Machines/Actions/Set Rolling Parameter")]
public class IsRollingParameterActionSO : StateActionSO
{
    public bool isRollingParam = default;

    public Moment whenToRun = default;
    protected override StateAction CreateAction() => new IsRollingParameterAction(isRollingParam);

}
public class IsRollingParameterAction : StateAction
{
    private Player _playerScript;

    private IsRollingParameterActionSO _originSO => (IsRollingParameterActionSO)base.OriginSO; // The SO this StateAction spawned from

    private bool isRollingParam;

    public IsRollingParameterAction(bool isRollingParam)
    {
        this.isRollingParam = isRollingParam;
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
        _playerScript.isRolling = isRollingParam;
    }

    public override void OnUpdate() { }

}
