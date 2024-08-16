using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : Character
{
    public Core Core;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    public EnemyPropertiesConfigSO entityData;
    [SerializeField]
    public StatsConfigSO entityStats;
    [SerializeField]
    public AnimationEventHandler animationEventHandler;

    [SerializeField] public GameObject alertSymbol, lookForPlayerSymbol;

    [NonSerialized] public bool shouldTransit = false;
    [NonSerialized] public Vector2 movementVector; //Final movement vector, manipulated by the StateMachine actions
    [NonSerialized] public bool attackFinished = true;

    protected virtual void OnEnable()
    {
        animationEventHandler.OnFinish += AttackFinished;
    }

    protected virtual void OnDisable()
    {
        animationEventHandler.OnFinish -= AttackFinished;
    }

    private void AttackFinished() => attackFinished = true;

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual void OnDrawGizmos()
    {
        if (Core != null)
        {

            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
        }
    }

}
