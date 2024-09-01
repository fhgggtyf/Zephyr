using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IncrementJumpCountAction", menuName = "State Machines/Actions/Increment Jumps")]
public class IncrementJumpActionSO : StateActionSO<IncrementJumpAction>
{
    public bool incrementWhenIncremented = false;
}

public class IncrementJumpAction : StateAction
{
    private Player _player;

    private IncrementJumpActionSO _originSO => (IncrementJumpActionSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnUpdate() { }

    public override void OnStateEnter()
    {
        if (_player.jumpIncremented == _originSO.incrementWhenIncremented)
        {
            _player.jumpCount++;
            _player.jumpIncremented = true;
        }
    }
}
