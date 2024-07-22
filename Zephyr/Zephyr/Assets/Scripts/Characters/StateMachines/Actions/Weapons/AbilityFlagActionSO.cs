using UnityEngine;
using UnityEngine.Events;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilityFlagHandle", menuName = "State Machines/Actions/Ability Flag")]
public class AbilityFlagActionSO : StateActionSO<AbilityFlagAction> 
{

}

public class AbilityFlagAction : StateAction
{

    private Player _player;

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnStateEnter()
    {
        _player.isAbilityFinished = false;
    }

    public override void OnStateExit()
    {
        _player.isAbilityFinished = true;
    }

    public override void OnUpdate()
    {
    }
}
