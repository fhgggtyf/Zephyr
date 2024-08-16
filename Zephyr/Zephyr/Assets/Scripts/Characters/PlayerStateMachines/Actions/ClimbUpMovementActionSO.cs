using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "Climb up Action", menuName = "State Machines/Actions/ClimbUp")]
public class ClimbUpMovementActionSO : StateActionSO
{
    public float xOffset, yOffset;
    protected override StateAction CreateAction() => new ClimbUpMovementAction(xOffset, yOffset);
}

public class ClimbUpMovementAction : StateAction
{
    private float _xOffset, _yOffset;

    private Vector2 _prevPos, _postPos;

    private CollisionSenses _collisionSenses;

    private Player _player;

    protected Movement Movement
    {
        get => movement ?? _player.Core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    public ClimbUpMovementAction(float xOffset, float yOffset)
    {
        _xOffset = xOffset;
        _yOffset = yOffset;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
        _collisionSenses = _player.Core.GetCoreComponent(ref _collisionSenses);
    }

    public override void OnStateEnter()
    {
        _prevPos = _collisionSenses.OffClimbCheck.position;
        _postPos = _prevPos + new Vector2(_xOffset * Movement.FacingDirection, _yOffset);
    }

    public override void OnStateExit()
    {
        Movement.ForceChangePosition(_postPos);
    }

    public override void OnUpdate()
    {
    }
}
