using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Can Jump")]
public class CanJumpConditionSO : StateConditionSO<CanJumpCondition>
{
}

public class CanJumpCondition : Condition
{
    private Player _player;
    private PlayerStatsManager _statsManager;

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
        _statsManager = stateMachine.GetComponent<PlayerStatsManager>();
    }

    protected override bool Statement()
    {
        Debug.Log("Jump count con?: " + (_player.jumpCount < _statsManager.GetMaxJumps()));
        return _player.jumpCount < _statsManager.GetMaxJumps();
    }

    public override void OnTransitionFailed()
    {
        Debug.Log("Cannot jump anymore");
    }
}
