using UnityEngine;
using UnityEngine.Events;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "FlipCheck", menuName = "State Machines/Actions/FlipCheck")]
public class CheckIfShouldFlipSO : StateActionSO<CheckIfShouldFlip> {
    public event UnityAction FlipEvent = delegate { };

    public void InvokeEvent()
    {
        FlipEvent.Invoke();
    }
}

public class CheckIfShouldFlip: StateAction
{
    private Player _player;

    protected Movement Movement
    {
        get => movement ?? _player.Core.GetCoreComponent(ref movement);
    }

    private Movement movement;
    private CheckIfShouldFlipSO _originSO => (CheckIfShouldFlipSO)base.OriginSO; // The SO this Condition spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();

        Movement.FacingDirection = 1;
    }

    public override void OnStateEnter()
    {
        _originSO.FlipEvent += Movement.Flip;
    }
    public override void OnStateExit()
    {
        _originSO.FlipEvent -= Movement.Flip;
    }

    public override void OnUpdate()
    {
        if (_player.InputVector.x != 0 && _player.InputVector.x != Movement.FacingDirection)
        {
            _originSO.InvokeEvent();
        }
    }
}
