using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ApplyMovementVector", menuName = "State Machines/Actions/Enemies/Apply Movement Vector")]
public class GeneralEnemyApplyMovementActionSO : StateActionSO<GeneralEnemyApplyMovementAction>
{ }
public class GeneralEnemyApplyMovementAction : StateAction
{
    private NonPlayerCharacter _npc;

    protected Movement Movement
    {
        get => movement ?? _npc.Core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    //public Rigidbody2D RB { get; private set; }

    public override void Awake(StateMachine stateMachine)
    {
        _npc = stateMachine.GetComponent<NonPlayerCharacter>();
        //RB = stateMachine.GetComponent<Rigidbody2D>();
    }

    public override void OnUpdate()
    {
        Movement.SetVelocity(_npc.movementVector);
        //_player.movementVector = RB.velocity;
    }
}
