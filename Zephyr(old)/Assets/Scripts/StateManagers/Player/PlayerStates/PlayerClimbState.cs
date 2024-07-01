using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerBaseState
{
    private Movement movement;
    private CollisionSenses collisionSenses;

    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;
    private Vector2 workspace;

    private bool isHanging;
    private bool isClimbing;
    private bool jumpInput;

    public PlayerClimbState(PlayerStateMachine currentContext, string animBoolName) : base(currentContext, animBoolName)
    {
        InitializeSubstate();
        IsRootState = true;
    }

    public override void EnterState()
    {
        base.EnterState();

        Movement?.SetVelocityZero();
        Player.transform.position = detectedPos;
        cornerPos = DetermineCornerPosition();

        startPos.Set(cornerPos.x - (Movement.FacingDirection * _ctx.Data.startOffset.x), cornerPos.y - _ctx.Data.startOffset.y);
        stopPos.Set(cornerPos.x + (Movement.FacingDirection * _ctx.Data.stopOffset.x), cornerPos.y + _ctx.Data.stopOffset.y);

        Player.transform.position = startPos;

    }

    public override void ExitState()
    {
        base.ExitState();

        isHanging = false;

        if (isClimbing)
        {
            Player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {

        if (isAnimationFinished)
        {

            _ctx.SwitchState(this, _ctx.Factory.Grounded());

        }
        else
        {
            xInput = Player.InputHandler.NormInputX;
            yInput = Player.InputHandler.NormInputY;
            jumpInput = Player.InputHandler.JumpInput;

            Movement?.SetVelocityZero();
            Player.transform.position = startPos;

            if (xInput == Movement.FacingDirection && isHanging && !isClimbing)
            {
                CheckForSpace();
                isClimbing = true;
                Player.Anim.SetBool("climbLedge", true);
            }
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                _ctx.SwitchState(this, _ctx.Factory.InAir());
            }
            else if (jumpInput && !isClimbing)
            {
                Player.capabilities[(int)Capability.jump].CapabilityAction();
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void InitializeSubstate()
    {

    }


    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        Player.Anim.SetBool("RopeClimb", false);
    }

    private void CheckForSpace()
    {
        isTouchingCeiling = Physics2D.Raycast(cornerPos + (Vector2.up * 0.015f) + (Vector2.right * Movement.FacingDirection * 0.015f), Vector2.up, _ctx.Data.standColliderHeight, CollisionSenses.WhatIsGround);
        Player.Anim.SetBool("isTouchingCeiling", isTouchingCeiling);
    }


    private Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(CollisionSenses.WallCheck.position, Vector2.right * Movement.FacingDirection, CollisionSenses.WallCheckDistance, CollisionSenses.WhatIsGround);
        float xDist = xHit.distance;
        workspace.Set((xDist + 0.015f) * Movement.FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(CollisionSenses.LedgeCheckHorizontal.position + (Vector3)(workspace), Vector2.down, CollisionSenses.LedgeCheckHorizontal.position.y - CollisionSenses.WallCheck.position.y + 0.015f, CollisionSenses.WhatIsGround);
        float yDist = yHit.distance;

        workspace.Set(CollisionSenses.WallCheck.position.x + (xDist * Movement.FacingDirection), CollisionSenses.LedgeCheckHorizontal.position.y - yDist);
        return workspace;
    }

}
