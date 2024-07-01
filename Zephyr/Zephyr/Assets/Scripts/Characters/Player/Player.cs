using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;

    //These fields are read and manipulated by the StateMachine actions
    [NonSerialized] public Vector2 InputVector;
    [NonSerialized] public bool jumpInput;
    [NonSerialized] public bool extraActionInput;
    [NonSerialized] public bool attackInput;
    [NonSerialized] public Vector2 movementVector; //Final movement vector, manipulated by the StateMachine actions
    [NonSerialized] public ControllerColliderHit lastHit;
    [NonSerialized] public int facingDirection;
    [NonSerialized] public bool isRunning; // Used when using the keyboard to run, brings the normalised speed to 1

    public const float GRAVITY_MULTIPLIER = 3f;

    public Core Core { get; private set; }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        lastHit = hit;
    }
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMove;
        _inputReader.JumpEvent += OnJumpInitiated;
        _inputReader.JumpCanceledEvent += OnJumpCanceled;
        _inputReader.RunEvent += OnRun;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.JumpEvent -= OnJumpInitiated;
        _inputReader.JumpCanceledEvent -= OnJumpCanceled;
        _inputReader.RunEvent -= OnRun;
    }

    // Update is called once per frame
    void Update() { }

    private void OnMove(Vector2 movement)
    {
        InputVector = new Vector2(Math.Sign(movement.x) * (Math.Abs(movement.x) >= 1 ? Math.Abs(movement.x) : 1), Math.Sign(movement.y) * (Math.Abs(movement.y) >= 1 ? Math.Abs(movement.y) : 1)) ;
    }
    private void OnRun(Vector2 movement)
    {
        isRunning = true;
        InputVector = new Vector2(Math.Sign(movement.x) * (Math.Abs(movement.x) >= 1 ? Math.Abs(movement.x) : 1), Math.Sign(movement.y) * (Math.Abs(movement.y) >= 1 ? Math.Abs(movement.y) : 1));
    }

    private void OnJumpInitiated()
    {
        jumpInput = true;
    }

    private void OnJumpCanceled()
    {
        jumpInput = false;
    }
}
