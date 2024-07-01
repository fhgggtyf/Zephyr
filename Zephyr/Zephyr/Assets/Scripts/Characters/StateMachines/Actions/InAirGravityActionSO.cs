using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "InAirGravity", menuName = "State Machines/Actions/In Air Gravity")]
public class InAirGravityActionSO : StateActionSO<InAirGravityAction>
{
    [Tooltip("Vertical movement pulling down the player to get it down")]
    public float verticalPull = -9.8f;
}

public class InAirGravityAction : StateAction
{
    //Component references
    private Player _player;

    private InAirGravityActionSO _originSO => (InAirGravityActionSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnStateEnter() { }

    public override void OnUpdate()
    {
        if (_player.movementVector.y >= -20)
        {
            _player.movementVector.y += _originSO.verticalPull * Time.deltaTime * Player.GRAVITY_MULTIPLIER;
        }

    }
}
