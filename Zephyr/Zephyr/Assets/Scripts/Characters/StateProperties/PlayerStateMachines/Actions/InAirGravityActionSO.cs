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
    private CollisionSenses _collisionSenses;
    private InAirGravityActionSO _originSO => (InAirGravityActionSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
        _collisionSenses = _player.Core.GetCoreComponent(ref _collisionSenses);
    }

    public override void OnStateEnter() { }

    public override void OnUpdate()
    {
        if (_player.movementVector.y >= -20 && !_collisionSenses.Ground)
        {
            _player.movementVector.y += (_player.movementVector.y >= 0 ? 1 : 2) * _originSO.verticalPull * Time.deltaTime * GenericPhysicsData.GRAVITY_MULTIPLIER;
        }

    }
}
