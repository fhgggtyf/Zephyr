using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "OnGroundFriction", menuName = "State Machines/Actions/General/OnGroundFriction")]
public class OnGroundFrictionSO : StateActionSO<OnGroundFriction>
{
    public float horizontalFriction = -5f;
}
public class OnGroundFriction : StateAction
{
    private Character _character;
    private CollisionSenses _collisionSenses;
    private OnGroundFrictionSO _originSO => (OnGroundFrictionSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
        _collisionSenses = _character.Core.GetCoreComponent(ref _collisionSenses);
    }

    public override void OnStateEnter() { }

    public override void OnUpdate()
    {
        if (_collisionSenses.Ground)
        {
            _character.movementVector.x += (_character.movementVector.x >= 0 ? 1 : -1) * _originSO.horizontalFriction * Time.deltaTime;
        }

    }
}