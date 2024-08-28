using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "InAirGravity", menuName = "State Machines/Actions/General/In Air Gravity")]
public class InAirGravityActionSO : StateActionSO<InAirGravityAction>
{
    [Tooltip("Vertical movement pulling down the Character to get it down")]
    public float verticalPull = -9.8f;
}

public class InAirGravityAction : StateAction
{
    //Component references
    private Character _character;
    private CollisionSenses _collisionSenses;
    private InAirGravityActionSO _originSO => (InAirGravityActionSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
        _collisionSenses = _character.Core.GetCoreComponent(ref _collisionSenses);
    }

    public override void OnStateEnter() { }

    public override void OnUpdate()
    {
        if (_character.movementVector.y >= -20 && !_collisionSenses.Ground)
        {
            _character.movementVector.y += (_character.movementVector.y >= 0 ? 1 : 2) * _originSO.verticalPull * Time.deltaTime * GenericPhysicsData.GRAVITY_MULTIPLIER;
        }

    }
}
