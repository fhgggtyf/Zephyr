using UnityEngine;


public class CollisionSenses : CoreComponent
{

    protected Movement Movement
    {
        get => movement ?? _player.Core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    #region Check Transforms

    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        private set => groundCheck = value;
    }
    public Transform ClimbableCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(climbableCheck, core.transform.parent.name);
        private set => climbableCheck = value;
    }
    public Transform OffClimbCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(offClimbCheck, core.transform.parent.name);
        private set => offClimbCheck = value;
    }
    public Transform CeilingCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name);
        private set => ceilingCheck = value;
    }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float ClimbableCheckDistance { get => climbableCheckDistance; set => climbableCheckDistance = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }


    [SerializeField] private Player _player;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform climbableCheck;
    [SerializeField] private Transform offClimbCheck;
    [SerializeField] private Transform ceilingCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float offClimbCheckRadius;
    [SerializeField] private float climbableCheckDistance;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsClimbable;

    #endregion

    public bool Ceiling
    {
        get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, whatIsGround);
    }

    public bool Ground
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    }
    public bool OffClimb
    {
        get => !Physics2D.OverlapCircle(offClimbCheck.position, offClimbCheckRadius, whatIsClimbable);
    }

    public bool ClimbableFront
    {
        get => Physics2D.Raycast(ClimbableCheck.position, Vector2.right * Movement.FacingDirection, climbableCheckDistance, whatIsClimbable);
    }

}

