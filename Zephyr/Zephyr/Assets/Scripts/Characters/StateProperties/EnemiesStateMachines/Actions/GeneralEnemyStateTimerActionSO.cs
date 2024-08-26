using UnityEngine;
using UnityEngine.Events;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "TimerAction", menuName = "State Machines/Actions/Enemies/Set Timer")]
public class GeneralEnemyStateTimerActionSO : StateActionSO
{
    public float stateTime;

    protected override StateAction CreateAction() => new GeneralEnemyStateTimerAction(stateTime);
}
public class GeneralEnemyStateTimerAction : StateAction
{
    private readonly float _time;
    private float _timer;

    private NonPlayerCharacter _npc;

    public override void Awake(StateMachine stateMachine)
    {
        _npc = stateMachine.GetComponent<NonPlayerCharacter>();
    }

    public GeneralEnemyStateTimerAction(float time)
    {
        _time = time;
    }

    public override void OnStateEnter()
    {
        _npc.shouldTransit = false;
        _timer = 0;

    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= _time)
        {
            _npc.shouldTransit = true;
        }
    }

    public override void OnStateExit()
    {
        _npc.shouldTransit = true;

    }
}


