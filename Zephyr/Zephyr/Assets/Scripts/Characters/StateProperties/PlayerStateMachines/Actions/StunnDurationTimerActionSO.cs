using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "StunTimer", menuName = "State Machines/Actions/StunTimer")]
public class StunnDurationTimerActionSO : StateActionSO<StunnDurationTimerAction>
{

}

public class StunnDurationTimerAction : StateAction
{
    private Damageable _damageable;
    private Player _player;
    private PlayerStatsManager _statsManager;

    private float _timer;

    public override void Awake(StateMachine stateMachine)
    {
        _damageable = stateMachine.GetComponent<Damageable>();
        _player = stateMachine.GetComponent<Player>();
        _statsManager = stateMachine.GetComponent<PlayerStatsManager>();
    }

    public override void OnStateEnter()
    {
        _timer = 0;
        _player.stunOver = false;
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= CalculateStunTime(_damageable.lastHitData.AbilityParam.stunDuration, _statsManager.GetCurrentTenacity()))
        {
            _player.stunOver = true;
        }
    }

    private float CalculateStunTime(float baseTime, float tenacity)
    {
        return baseTime * (1 - tenacity / 100);
    }
}
