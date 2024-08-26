using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using Moment = Zephyr.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(fileName = "GeneralEnemyTurnAction", menuName = "State Machines/Actions/Enemies/Turn")]

public class GeneralEnemyTurnActionSO : StateActionSO<GeneralEnemyTurnAction>
{
    public Moment whenToRun = default;
}


public class GeneralEnemyTurnAction : StateAction
{
    private NonPlayerCharacter _npc;

    protected Movement Movement
    {
        get => movement ?? _npc.Core.GetCoreComponent(ref movement);
    }

    private Movement movement;
    private GeneralEnemyTurnActionSO _originSO => (GeneralEnemyTurnActionSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _npc = stateMachine.GetComponent<NonPlayerCharacter>();
    }

    public override void OnStateEnter()
    {
        if (_originSO.whenToRun == SpecificMoment.OnStateEnter)
            Flip();
    }

    public override void OnStateExit()
    {
        if (_originSO.whenToRun == SpecificMoment.OnStateExit)
            Flip();
    }

    private void Flip()                     
    {
        Movement.Flip();
        
    }

    public override void OnUpdate() { }

}