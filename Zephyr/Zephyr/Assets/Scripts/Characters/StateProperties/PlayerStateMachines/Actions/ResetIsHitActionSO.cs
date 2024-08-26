using System.Collections;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ResetIsHitAction", menuName = "State Machines/Actions/ResetIsHit")]
public class ResetIsHitActionSO : StateActionSO<ResetIsHit>
{
}

public class ResetIsHit : StateAction
{
    private Damageable _damageable;

    public override void Awake(StateMachine stateMachine)
    {
        _damageable = stateMachine.GetComponent<Damageable>();
    }

    public override void OnStateEnter()
    {
        _damageable.GetHit = false;
    }
    public override void OnUpdate()
    {
        Debug.Log(_damageable.GetHit);
    }
}
