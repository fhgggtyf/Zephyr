using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/has Jumped")]
public class HasJumpConditionSO : StateConditionSO<HasJumpCondition>
{
}

public class HasJumpCondition : Condition
{
    private Player _player;

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    protected override bool Statement()
    {
        return _player.jumpIncremented;
    }

    public override void OnTransitionFailed()
    {
        Debug.Log("Cannot jump anymore");
    }
}