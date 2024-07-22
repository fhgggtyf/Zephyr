using UnityEngine;
using UnityEngine.Events;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "DoubleJumpTimerAction", menuName = "State Machines/Actions/Set DoubleJumpTimer")]
public class DoubleJumpTimerActionSO : StateActionSO
{
    public float stateTime;

    protected override StateAction CreateAction() => new DoubleJumpTimerAction(stateTime);
}

public class DoubleJumpTimerAction : StateAction
{
    private readonly float _time;
    private float _timer;

    private Player _player;

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public DoubleJumpTimerAction(float time)
    {
        _time = time;
    }

    public override void OnStateEnter()
    {
        _player.isAbilityFinished = false;
        _timer = 0;

    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= _time)
        {
            _player.isAbilityFinished = true;
        }
    }

    public override void OnStateExit()
    {
        _player.isAbilityFinished = true;

    }

}
