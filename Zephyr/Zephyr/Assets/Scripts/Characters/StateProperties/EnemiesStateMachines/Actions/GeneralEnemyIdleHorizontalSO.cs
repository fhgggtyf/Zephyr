using System;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Horizontal", menuName = "State Machines/Actions/Enemies/Idle Horizontal")]
public class GeneralEnemyIdleHorizontalSO : StateActionSO<GeneralEnemyIdleHorizontal>
{ }

public class GeneralEnemyIdleHorizontal : StateAction
{
    private NonPlayerCharacter _npc;

    public override void Awake(StateMachine stateMachine)
    {
        _npc = stateMachine.GetComponent<NonPlayerCharacter>();
    }

    public override void OnUpdate()
    {
        _npc.movementVector.x = 0;
    }
}