using UnityEngine;
using UnityEngine.Events;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "GroundEnemyLookForpLAYER", menuName = "State Machines/Actions/Enemies/GroundEnemies/LookForPlayer", order = 0)]

public class GroundEnemyLookForPlayerActionSO : StateActionSO<GroundEnemyLookForPlayerAction>
{
    public float frontTime, backTime;

    public event UnityAction FlipEvent = delegate { };

    public void InvokeEvent()
    {
        FlipEvent.Invoke();
    }
}

public class GroundEnemyLookForPlayerAction : StateAction
{
    private float _timer;
    private bool _flipped;
    protected Movement Movement
    {
        get => movement ?? _npc.Core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    private NonPlayerCharacter _npc;

    private GroundEnemyLookForPlayerActionSO _originSO => (GroundEnemyLookForPlayerActionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _npc = stateMachine.GetComponent<NonPlayerCharacter>();
    }

    public override void OnStateEnter()
    {
        _timer = 0;
        _flipped = false;
        _originSO.FlipEvent += Movement.Flip;
        _npc.shouldTransit = false;
    }

    public override void OnStateExit()
    {
        _originSO.FlipEvent -= Movement.Flip;
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= _originSO.frontTime && _flipped == false)
        {
            _flipped = true;
            _originSO.InvokeEvent();
        }
        else if (_flipped == true && _timer >= _originSO.frontTime + _originSO.backTime)
        {
            _npc.shouldTransit = true;
        }
    }
}