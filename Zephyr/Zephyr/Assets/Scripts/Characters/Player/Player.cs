using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] public Core Core;
    [SerializeField] private Weapon primaryWeapon;
    [SerializeField] private Weapon secondaryWeapon;

    //These fields are read and manipulated by the StateMachine actions
    [NonSerialized] public Vector2 InputVector;
    [NonSerialized] public bool jumpInput;
    [NonSerialized] public bool extraActionInput;
    [NonSerialized] public bool primaryAttackInput;
    [NonSerialized] public bool secondaryAttackInput;
    [NonSerialized] public Vector2 movementVector; //Final movement vector, manipulated by the StateMachine actions
    [NonSerialized] public ControllerColliderHit lastHit;
    //[NonSerialized] public int facingDirection;
    [NonSerialized] public bool isRunning; // Used when using the keyboard to run, brings the normalised speed to 1
    [NonSerialized] public bool isCrouching;
    [NonSerialized] public bool isRolling;

    public const float GRAVITY_MULTIPLIER = 3f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        lastHit = hit;
    }
    private void Awake() {
        primaryWeapon.SetCore(Core);
        secondaryWeapon.SetCore(Core);
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMove;
        _inputReader.JumpEvent += OnJumpInitiated;
        _inputReader.JumpCanceledEvent += OnJumpCanceled;
        _inputReader.RunEvent += OnRun;
        _inputReader.InteractEvent += OnInteract;
        _inputReader.CrouchEvent += OnCrouch;
        _inputReader.CrouchCanceledEvent += OnCrouchCanceled;
        _inputReader.RollEvent += OnRoll;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.JumpEvent -= OnJumpInitiated;
        _inputReader.JumpCanceledEvent -= OnJumpCanceled;
        _inputReader.RunEvent -= OnRun;
        _inputReader.InteractEvent -= OnInteract;
        _inputReader.CrouchEvent -= OnCrouch;
        _inputReader.CrouchCanceledEvent -= OnCrouchCanceled;
        _inputReader.RollEvent -= OnRoll;
    }

    // Update is called once per frame
    void Update() { }

    private void OnMove(Vector2 inputMovement)
    {
        InputVector = new Vector2(Math.Sign(inputMovement.x) * (Math.Abs(inputMovement.x) >= 1 ? Math.Abs(inputMovement.x) : 1), Math.Sign(inputMovement.y) * (Math.Abs(inputMovement.y) >= 1 ? Math.Abs(inputMovement.y) : 1)) ;
    }
    private void OnRun(Vector2 inputMovement)
    {
        isRunning = true;
        InputVector = new Vector2(Math.Sign(inputMovement.x) * (Math.Abs(inputMovement.x) >= 1 ? Math.Abs(inputMovement.x) : 1), Math.Sign(inputMovement.y) * (Math.Abs(inputMovement.y) >= 1 ? Math.Abs(inputMovement.y) : 1));
    }

    private void OnCrouch()
    {
        isCrouching = true;
    }
    private void OnCrouchCanceled()
    {
        isCrouching = false;
    }

    private void OnJumpInitiated()
    {
        jumpInput = true;
    }

    private void OnJumpCanceled()
    {
        jumpInput = false;
    }

    private void OnRoll()
    {
        isRolling = true;
    }

    private void OnInteract()
    {
        Core.GetCoreComponent<InteractableDetector>().TryInteract();
    }
}
