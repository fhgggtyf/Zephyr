using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "FacePlayer", menuName = "State Machines/Actions/Enemies/Face Player")]
public class GeneralEnemyFacePlayerActionSO : StateActionSO<GeneralEnemyFacePlayerAction>
{
    
}          
public class GeneralEnemyFacePlayerAction : StateAction
{
    protected Movement Movement
    {
        get => movement ?? _npc.Core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    private NonPlayerCharacter _npc;

    private Damageable _damageable;

    public override void Awake(StateMachine stateMachine)
    {
        _damageable = stateMachine.GetComponent<Damageable>();
        _npc = stateMachine.GetComponent<NonPlayerCharacter>();
    }

    public override void OnStateEnter()
    {
        if (Movement.FacingDirection == (_npc.transform.position.x - _damageable.lastHitData.Source.transform.position.x >= 0 ? 1 : -1))
        {
            Movement.Flip();
        }

    }

    public override void OnUpdate()
    {

    }

}
