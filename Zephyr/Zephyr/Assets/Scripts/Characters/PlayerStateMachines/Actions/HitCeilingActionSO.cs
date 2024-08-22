using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "PerfectBounce", menuName = "State Machines/Actions/PerfectBounce")]
public class HitCeilingActionSO : StateActionSO<HitCeilingAction>
{
}

public class HitCeilingAction : StateAction
{
    //Component references
    private Player _player;

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnStateEnter()
    {
        _player.movementVector.y = -_player.movementVector.y + 3;
    }

    public override void OnUpdate() { }
}
