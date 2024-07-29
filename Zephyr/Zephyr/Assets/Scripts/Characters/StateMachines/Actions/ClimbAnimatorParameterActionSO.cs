using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using Moment = Zephyr.StateMachine.StateAction.SpecificMoment;

/// <summary>
/// Flexible StateActionSO for the StateMachine which allows to set any parameter on the Animator, in any moment of the state (OnStateEnter, OnStateExit, or each OnUpdate).
/// </summary>
[CreateAssetMenu(fileName = "ClimbAnimatorParameterAction", menuName = "State Machines/Actions/Set climb Animator Parameter")]
public class ClimbAnimatorParameterActionSO : StateActionSO
{
    public string[] animName = default;
    public Moment whenToRun = default;
    public float playSpeed = 1;
    public bool replayIfSame = default;

    protected override StateAction CreateAction() => new ClimbAnimatorParameterAction(animName, playSpeed, replayIfSame);

}
public class ClimbAnimatorParameterAction : StateAction
{
    private AnimationManager _animManager;
    private Player _player;

    protected ClimbInteraction ClimbInteraction
    {
        get => climbInteraction ?? _player.Core.GetCoreComponent(ref climbInteraction);
    }

    private ClimbInteraction climbInteraction;
    private ClimbAnimatorParameterActionSO _originSO => (ClimbAnimatorParameterActionSO)base.OriginSO; // The SO this StateAction spawned from
    private string[] _animNames;
    private string _animPlaying;
    private float _playSpeed;
    private bool _replayIfSame;

    public ClimbAnimatorParameterAction(string[] param, float playSpeedParam, bool replayIfSame)
    {
        _animNames = param;
        _playSpeed = playSpeedParam;
        _replayIfSame = replayIfSame;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _animManager = stateMachine.GetComponent<AnimationManager>();
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnStateEnter()
    {
        EnterChecks();

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
        _animManager.ChangeAnimState(_animPlaying, _playSpeed, _replayIfSame);
    }

    public override void OnUpdate() { }

    public void EnterChecks()
    {
        _animPlaying = ClimbInteraction.Climbable.climbType == ClimbTypes.Rope ? _animNames[0] : _animNames[1];

        _playSpeed = _player.movementVector.y >= 0 ? Mathf.Abs(_playSpeed) : -Mathf.Abs(_playSpeed);

    }

}
