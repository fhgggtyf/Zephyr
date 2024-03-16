using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{

    [SerializeField] private float _maxSpeed = 4f;

    private Vector2 _direction;
    private Vector2 _desiredVelocity;
    private Vector2 _velocity;
    private Rigidbody2D _body;
    private Ground _ground;
    private short _isFacingRight = 1;

    private float _maxSpeedChange;
    [SerializeField] private float _acceleration = 4f;
    private bool _onGround;

    bool _desiredJump;
    bool _desiredRoll;

    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private int _maxAirJumps = 2;

    private int _jumpPhase;
    private float _defaultGravityScale = 3f;

    [SerializeField] private float _rollDesiredDistance = 0.5f;
    private float _rollDecceleration = 10f;

    [SerializeField] private float _dashDesiredDistance = 1.5f;
    private float _dashDecceleration = 80f;

    PlayerStateFactory _states;

    [SerializeField] public InputController input = null;

    PlayerBaseState _currentState;

    public PlayerBaseState CurrentState { get => _currentState; set => _currentState = value; }
    public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }
    public Vector2 Direction { get => _direction; set => _direction = value; }  
    public float DirectionX { get => _direction.x; set => _direction.x = value; }
    public Vector2 DesiredVelocity { get => _desiredVelocity; set => _desiredVelocity = value; }
    public float DesiredVelocityX { get => _desiredVelocity.x; set => _desiredVelocity.x = value; }
    public Vector2 Velocity { get => _velocity; set => _velocity = value; }
    public float VelocityX { get => _velocity.x; set => _velocity.x = value; }
    public float VelocityY { get => _velocity.y; set => _velocity.y = value; }
    public Rigidbody2D Body { get => _body; set => _body = value; }
    public Ground Ground { get => _ground; set => _ground = value; }
    public float MaxSpeedChange { get => _maxSpeedChange; set => _maxSpeedChange = value; }
    public float Acceleration { get => _acceleration; set => _acceleration = value; }
    public bool OnGround { get => _onGround; set => _onGround = value; }
    public bool DesiredJump { get => _desiredJump; set => _desiredJump = value; }
    public bool DesiredRoll { get => _desiredRoll; set => _desiredRoll = value; }
    public float JumpHeight { get => _jumpHeight; set => _jumpHeight = value; }
    public int MaxJumps { get => _maxAirJumps; set => _maxAirJumps = value; }
    public int JumpPhase { get => _jumpPhase; set => _jumpPhase = value; }
    public float DefaultGravityScale { get => _defaultGravityScale; set => _defaultGravityScale = value; }
    public PlayerStateFactory States { get => _states; set => _states = value; }
    public float RollDesiredDistance { get => _rollDesiredDistance; set => _rollDesiredDistance = value; }
    public float RollDecceleration { get => _rollDecceleration; set => _rollDecceleration = value; }
    public float DashDesiredDistance { get => _dashDesiredDistance; set => _dashDesiredDistance = value; }
    public float DashDecceleration { get => _dashDecceleration; set => _dashDecceleration = value; }
    public short IsFacingRight { get => _isFacingRight; set => _isFacingRight = value; }

    private void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
        Ground = GetComponent<Ground>();
        DesiredJump = false;
        DesiredRoll = false;
        JumpPhase = 0;

        States = new PlayerStateFactory(this);
        CurrentState = States.Grounded();
        CurrentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        CheckJump();
        CheckRoll();
        OnGround = Ground.GetOnGround();
        CurrentState.UpdateStates();
    }

    private void FixedUpdate()
    {
        CurrentState.FixedUpdateStates();
    }

    void CheckJump()
    {
        DesiredJump |= input.RetrieveJumpInput();
    }

    void CheckRoll()
    {
        DesiredRoll |= input.RetrieveRollInput();
    }

}
