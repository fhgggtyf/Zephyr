using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "GroundGravity", menuName = "State Machines/Actions/General/Ground Gravity")]
public class GroundGravityActionSO : StateActionSO<GroundGravityAction>
{
    [Tooltip("Vertical movement pulling down the Character to keep it anchored to the ground.")]
    public float verticalPull = -5f;
}

public class GroundGravityAction : StateAction
{
    //Component references
    private Character _character;

    private GroundGravityActionSO _originSO => (GroundGravityActionSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    public override void OnUpdate()
    {
        _character.movementVector.y = _originSO.verticalPull;
    }
}
