using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ApplyMovementVector", menuName = "State Machines/Actions/Apply Movement Vector")]
public class ApplyMovementVectorActionSO : StateActionSO<ApplyMovementVectorAction> { }

public class ApplyMovementVectorAction : StateAction
{
    private Player _player;

    public Rigidbody2D RB { get; private set; }

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
        RB = stateMachine.GetComponent<Rigidbody2D>();
    }

    public override void OnUpdate()
    {
        RB.velocity = _player.movementVector;
        _player.movementVector = RB.velocity;
    }
}


