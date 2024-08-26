using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ChangeCrouchCollider", menuName = "State Machines/Actions/ChangeCrouchCollider")]
public class ChangeCrouchColliderActionSO : StateActionSO<ChangeCrouchColliderAction>
{
}

public class ChangeCrouchColliderAction : StateAction
{
    private Collider2D[] _colliders;

    public override void Awake(StateMachine stateMachine)
    {
        _colliders = stateMachine.GetComponents<PolygonCollider2D>();
    }

    public override void OnStateEnter()
    {
        foreach(Collider2D coll in _colliders)
        {
            coll.enabled = !coll.enabled;
        }
    }

    public override void OnStateExit()
    {
        foreach (Collider2D coll in _colliders)
        {
            coll.enabled = !coll.enabled;
        }
    }

    public override void OnUpdate()
    {
    }
}
