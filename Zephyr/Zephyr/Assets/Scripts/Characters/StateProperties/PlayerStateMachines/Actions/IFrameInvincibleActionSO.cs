using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using System.Linq;

[CreateAssetMenu(fileName = "IFrameInvincible", menuName = "State Machines/Actions/Give IFrame")]
public class IFrameInvincibleActionSO : StateActionSO<IFrameInvincibleAction>
{
}

public class IFrameInvincibleAction : StateAction
{
    private Player _player;
    private Damageable _damageable;

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
        _damageable = stateMachine.GetComponent<Damageable>();
    }

    public override void OnStateEnter()
    {
        _player.animationEventHandler.OnIFrameActive += SetIFrame;
    }

    public override void OnStateExit()
    {
        _player.animationEventHandler.OnIFrameActive -= SetIFrame;
    }

    public override void OnUpdate()
    {
    }

    private void SetIFrame(bool param)
    {

        foreach (Collider2D coll in _player.GetComponents<PolygonCollider2D>().Where(n => n.enabled))
        {
            _damageable.Invincible = param;
        }


    }
}