using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using Moment = Zephyr.StateMachine.StateAction.SpecificMoment;

/// <summary>
/// Flexible StateActionSO for the StateMachine which allows to set any parameter on the Animator, in any moment of the state (OnStateEnter, OnStateExit, or each OnUpdate).
/// </summary>
[CreateAssetMenu(fileName = "SetTimerAction", menuName = "State Machines/Actions/Set State Timer")]
public class StateTimerActionSO : StateActionSO
{
    public float StateTimer;

    protected override StateAction CreateAction() => new StateTimerAction(StateTimer);

}
public class StateTimerAction : StateAction
{
    private Player _player;
    private readonly float _time;
    private float _timer;

    public StateTimerAction(float stateTimer)
    {
        _time = stateTimer;
    }
    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnStateEnter()
    {
        _timer = 0;
        _player.isAbilityFinished = false;
    }

    public override void OnStateExit()
    {
        _player.isAbilityFinished = true;
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _time)
        {
            _player.isAbilityFinished = true;
        }
    }

}
