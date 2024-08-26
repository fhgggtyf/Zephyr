using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using Moment = Zephyr.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(fileName = "JumpInputParameterAction", menuName = "State Machines/Actions/Set Jump Parameter")]
public class JumpInputParameterActionSO : StateActionSO
{
    public bool jumpInputParam = default;

    public Moment whenToRun = default;
    protected override StateAction CreateAction() => new JumpInputParameterAction(jumpInputParam);

}
public class JumpInputParameterAction : StateAction
{
    private Player _playerScript;

    private JumpInputParameterActionSO _originSO => (JumpInputParameterActionSO)base.OriginSO; // The SO this StateAction spawned from

    private bool _jumpInputParam;

    public JumpInputParameterAction(bool jumpInputParam)
    {
        this._jumpInputParam = jumpInputParam;
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
        _playerScript.jumpInput = _jumpInputParam;
    }

    public override void OnUpdate() { }

}
