using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "TriggerDeathEvent", menuName = "State Machines/Actions/General/TriggerDeathEvent")]

public class TriggerDeathEventActionSO : StateActionSO<TriggerDeathEventAction>
{
    public VoidEventChannelSO deathEventChannel;
}

public class TriggerDeathEventAction : StateAction
{
    private Character _character;

    private TriggerDeathEventActionSO _originSO => (TriggerDeathEventActionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    public override void OnStateEnter()
    {
        _character.animationEventHandler.OnFinish += BroadcastCharacterDead;
    }

    public override void OnStateExit()
    {
        _character.animationEventHandler.OnFinish -= BroadcastCharacterDead;
    }

    private void BroadcastCharacterDead() => _originSO.deathEventChannel.RaiseEvent();

    public override void OnUpdate()
    {
    }
}
