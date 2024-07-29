using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using Moment = Zephyr.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(fileName = "IsClimbingParameterAction", menuName = "State Machines/Actions/Set Climbing Parameter")]
public class IsClimbingParameterActionSO : StateActionSO
{
    public bool isClimbingParam = default;

    public Moment whenToRun = default;
    protected override StateAction CreateAction() => new IsClimbingParameterAction(isClimbingParam);

}
public class IsClimbingParameterAction : StateAction
{
    private Player _playerScript;

    private IsClimbingParameterActionSO _originSO => (IsClimbingParameterActionSO)base.OriginSO; // The SO this StateAction spawned from

    private bool isClimbingParam;

    public IsClimbingParameterAction(bool isClimbingParam)
    {
        this.isClimbingParam = isClimbingParam;
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
        _playerScript.isClimbing = isClimbingParam;
    }

    public override void OnUpdate() { }

}
