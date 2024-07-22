using UnityEngine;
using UnityEngine.Events;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetRunCostAction", menuName = "State Machines/Actions/Set Run State Cost")]
public class RunStaminaCostActionSO : StateActionSO
{
    [Tooltip("CostPerSecond")]
    public int baseCost = default;
    protected override StateAction CreateAction() => new RunStaminaCostAction(baseCost);
}

public class RunStaminaCostAction : StateAction
{
    private readonly int _baseStaminaCost;
    private StatsManager _statsManager;
    private Player _player;
    public RunStaminaCostAction(int cost)
    {
        _baseStaminaCost = cost;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _statsManager = stateMachine.GetComponent<StatsManager>();
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnStateEnter()
    {
        if (_player.isRunning)
        {
            _statsManager.CanRestoreStamina = false;
        }

    }

    public override void OnStateExit()
    {
        if (_player.isRunning)
        {
            _statsManager.updatedFlag = true;
            _statsManager.CanRestoreStamina = true;
        }
    }

    public override void OnUpdate()
    {
        if (_player.isRunning)
        {
            _statsManager.SpendStamina(_baseStaminaCost * Time.deltaTime);
        }
    }


}
