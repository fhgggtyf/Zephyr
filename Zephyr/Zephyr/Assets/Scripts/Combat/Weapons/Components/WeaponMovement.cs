using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : WeaponComponent<WeaponMovementData, AttackMovement>
{

    private Movement coreMovement;

    private float velocity;
    private Vector2 direction;

    private void HandleStartMovement()
    {
        velocity = currentAttackData.Velocity;
        direction = currentAttackData.Direction;

        SetVelocity();
    }

    private void HandleStopMovement()
    {
        velocity = 0f;
        direction = Vector2.zero;

        SetVelocity();
    }

    protected override void HandleEnter()
    {
        base.HandleEnter();

        velocity = 0f;
        direction = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (!isAttackActive)
            return;

        SetVelocityX();
    }

    private void SetVelocity()
    {
        coreMovement.SetVelocity(velocity, direction, coreMovement.FacingDirection);
    }

    private void SetVelocityX()
    {
        coreMovement.SetVelocityX((direction * velocity).x * coreMovement.FacingDirection);
    }

    protected override void Start()
    {
        base.Start();

        coreMovement = Core.GetCoreComponent<Movement>();

        AnimationEventHandler.OnStartMovement += HandleStartMovement;
        AnimationEventHandler.OnStopMovement += HandleStopMovement;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        AnimationEventHandler.OnStartMovement -= HandleStartMovement;
        AnimationEventHandler.OnStopMovement -= HandleStopMovement;
    }

}
