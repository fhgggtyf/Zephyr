using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "StunTimer", menuName = "State Machines/Actions/General/StunTimer")]
public class StunnDurationTimerActionSO : StateActionSO<StunnDurationTimerAction>
{

}

public class StunnDurationTimerAction : StateAction
{
    private Damageable _damageable;
    private Character character;
    private StatsManager _statsManager;

    private float _timer;

    public override void Awake(StateMachine stateMachine)
    {
        _damageable = stateMachine.GetComponent<Damageable>();
        character = stateMachine.GetComponent<Character>();
        _statsManager = stateMachine.GetComponent<StatsManager>();
    }

    public override void OnStateEnter()
    {
        _timer = 0;
        character.stunOver = false;
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= CalculateStunTime(_damageable.lastHitData.AbilityParam.stunDuration, _statsManager.GetCurrentTenacity()))
        {
            Debug.Log("timer = " + _timer);
            character.stunOver = true;
        }
    }

    private float CalculateStunTime(float baseTime, float tenacity)
    {
        Debug.Log(baseTime * (1 - tenacity / 100));
        return baseTime * (1 - tenacity / 100);
    }
}
