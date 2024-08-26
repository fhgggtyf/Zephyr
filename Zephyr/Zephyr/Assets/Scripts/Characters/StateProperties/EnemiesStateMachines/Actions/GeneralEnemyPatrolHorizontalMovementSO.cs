using System;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Horizontal", menuName = "State Machines/Actions/Enemies/Horizontal")]
public class GeneralEnemyPatrolHorizontalMovementSO : StateActionSO<GeneralEnemyPatrolHorizontalMovement>
{
    public MoveState moveState;
}

public class GeneralEnemyPatrolHorizontalMovement : StateAction
{
    protected Movement Movement
    {
        get => movement ?? _npc.Core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    private NonPlayerCharacter _npc;

    private float _runMultiplier;
    private GeneralEnemyPatrolHorizontalMovementSO _originSO => (GeneralEnemyPatrolHorizontalMovementSO)base.OriginSO; // The SO this StateAction spawned from

    public override void Awake(StateMachine stateMachine)
    {
        _npc = stateMachine.GetComponent<NonPlayerCharacter>();
    }

    public override void OnStateEnter()
    {
        switch (_originSO.moveState)
        {
            case MoveState.Walk:
                _runMultiplier = 1;
                break;
            case MoveState.Run:
                _runMultiplier = 1.6f;
                break;
        }

    }
    public override void OnUpdate()
    {
        _npc.movementVector.x = Movement.FacingDirection * _npc.entityData.BaseSpeed * _runMultiplier;
    }
}