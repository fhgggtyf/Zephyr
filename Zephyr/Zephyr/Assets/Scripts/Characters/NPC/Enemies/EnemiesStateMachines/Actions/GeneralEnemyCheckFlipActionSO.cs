using UnityEngine;
using UnityEngine.Events;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "FlipCheck", menuName = "State Machines/Actions/Enemies/FlipCheck")]
public class GeneralEnemyCheckFlipActionSO : StateActionSO<GeneralEnemyCheckFlipAction>
{
    public event UnityAction FlipEvent = delegate { };

    public void InvokeEvent()
    {
        FlipEvent.Invoke();
    }
}

public class GeneralEnemyCheckFlipAction : StateAction
{
    private NonPlayerCharacter _npc;

    protected Movement Movement
    {
        get => movement ?? _npc.Core.GetCoreComponent(ref movement);
    }

    private Movement movement;
    private GeneralEnemyCheckFlipActionSO _originSO => (GeneralEnemyCheckFlipActionSO)base.OriginSO; // The SO this Condition spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _npc = stateMachine.GetComponent<NonPlayerCharacter>();

        Movement.FacingDirection = 1;
    }

    public override void OnStateEnter()
    {
        _originSO.FlipEvent += Movement.Flip;
    }
    public override void OnStateExit()
    {
        _originSO.FlipEvent -= Movement.Flip;
    }

    public override void OnUpdate()
    {
        if (_npc.movementVector.x != 0 && _npc.movementVector.x * Movement.FacingDirection < 0)
        {
            _originSO.InvokeEvent();
        }
    }
}
