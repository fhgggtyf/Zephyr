using UnityEngine;
using UnityEngine.Events;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetRollDashCostAction", menuName = "State Machines/Actions/Set Roll Dash State Cost")]
public class RollDashTimeStaminaCostActionSO : StateActionSO
{
    public int baseCost = default;

    public float stateTimer;

    protected override StateAction CreateAction() => new RollDashStaminaCostAction(baseCost, stateTimer);
}

public class RollDashStaminaCostAction: StateAction
{
    private readonly int _baseStaminaCost;
    private Player _player;
    private readonly float _time;
    private float _timer;
    private StatsManager _statsManager;
    public RollDashStaminaCostAction(int cost, float stateTimer)
    {
        _baseStaminaCost = cost;
        _time = stateTimer;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
        _statsManager = stateMachine.GetComponent<StatsManager>();
    }

    public override void OnStateEnter()
    {
        _timer = 0;
        _player.isAbilityFinished = false;
        _statsManager.SpendStamina(_baseStaminaCost);
        _statsManager.CanRestoreStamina = false;
    }

    public override void OnStateExit()
    {
        _player.isAbilityFinished = true;
        _statsManager.CanRestoreStamina = true;
        _statsManager.ReturnStamina((int)(_baseStaminaCost * (1 - (_timer / _time))));
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _time)
        {
            _player.isAbilityFinished = true;
        }
    }


}
