using UnityEngine;
using UnityEngine.Events;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AttackFlagHandle", menuName = "State Machines/Actions/General/Attack Flag")]
public class AttackFlagActionSO : StateActionSO<AttackFlagAction>
{

}

public class AttackFlagAction : StateAction
{

    private Character _character;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    public override void OnStateEnter()
    {
        _character.isAttackFinished = false;
    }

    public override void OnStateExit()
    {
        _character.isAttackFinished = true;
    }

    public override void OnUpdate()
    {
    }
}
