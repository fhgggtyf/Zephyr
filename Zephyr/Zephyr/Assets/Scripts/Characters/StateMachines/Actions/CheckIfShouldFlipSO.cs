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
    public Rigidbody2D RB { get; private set; }
    private CheckIfShouldFlipSO _originSO => (CheckIfShouldFlipSO)base.OriginSO; // The SO this Condition spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
        RB = stateMachine.GetComponent<Rigidbody2D>();

        _player.facingDirection = 1;
    }

    public override void OnStateEnter()
    {
        _originSO.FlipEvent += Flip;
    }
    public override void OnStateExit()
    {
        _originSO.FlipEvent -= Flip;
    }

    public override void OnUpdate()
    {
        if (_player.InputVector.x != 0 && _player.InputVector.x != _player.facingDirection)
        {
            _originSO.InvokeEvent();
        }
    }

    public void Flip()
    {
        Debug.Log("flipping");
        _player.facingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
