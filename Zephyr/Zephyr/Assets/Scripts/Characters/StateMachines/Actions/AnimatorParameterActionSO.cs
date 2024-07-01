using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using Moment = Zephyr.StateMachine.StateAction.SpecificMoment;

/// <summary>
/// Flexible StateActionSO for the StateMachine which allows to set any parameter on the Animator, in any moment of the state (OnStateEnter, OnStateExit, or each OnUpdate).
/// </summary>
[CreateAssetMenu(fileName = "AnimatorParameterAction", menuName = "State Machines/Actions/Set Animator Parameter")]
public class AnimatorParameterActionSO : StateActionSO
{
    public string animName = default;
    public Moment whenToRun = default;

    protected override StateAction CreateAction() => new AnimatorParameterAction(animName);

}
public class AnimatorParameterAction : StateAction
{
    private AnimationManager _animManager;
    private AnimatorParameterActionSO _originSO => (AnimatorParameterActionSO)base.OriginSO; // The SO this StateAction spawned from
    private string _animName;

    public AnimatorParameterAction(string param)
    {
        _animName = param;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _animManager = stateMachine.GetComponent<AnimationManager>();
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
        _animManager.ChangeAnimState(_animName);
    }

    public override void OnUpdate() { }

}
