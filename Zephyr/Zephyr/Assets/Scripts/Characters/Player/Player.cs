using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] public Core Core;
    //[SerializeField] public AnimationEventHandler animationEventHandler;

    [SerializeField] public Weapon[] weapons;

    //These fields are read and manipulated by the StateMachine actions
    [NonSerialized] public Vector2 InputVector;
    [NonSerialized] public bool jumpInput;
    [NonSerialized] public bool extraActionInput;
    [NonSerialized] public bool[] attackInput;
    [NonSerialized] public Vector2 movementVector; //Final movement vector, manipulated by the StateMachine actions
    [NonSerialized] public ControllerColliderHit lastHit;
    //[NonSerialized] public int facingDirection;
    [NonSerialized] public bool isRunning; // Used when using the keyboard to run, brings the normalised speed to 1
    [NonSerialized] public bool isCrouching;
    [NonSerialized] public bool isRolling;
    [NonSerialized] public bool isAbilityFinished;
    [NonSerialized] public int jumpCount;
    [NonSerialized] public bool jumpIncremented;
    [NonSerialized] public bool isClimbing;

    public const float GRAVITY_MULTIPLIER = 3f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        lastHit = hit;
    }
    private void Awake() {
        foreach(Weapon i in weapons)
        {
            i.SetCore(Core);
        }

        attackInput = new bool[2];
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
        _inputReader.RollCanceledEvent += OnRollCanceled;
        _inputReader.PrimaryAttackEvent += OnPrimaryAttack;
        _inputReader.PrimaryAttackCanceledEvent += OnPrimaryAttackCanceled;
        _inputReader.SecondaryAttackEvent += OnSecondaryAttack;
        _inputReader.SecondaryAttackCanceledEvent += OnSecondaryAttackCanceled;
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
        _inputReader.RollCanceledEvent -= OnRollCanceled;
        _inputReader.PrimaryAttackEvent -= OnPrimaryAttack;
        _inputReader.PrimaryAttackCanceledEvent -= OnPrimaryAttackCanceled;
        _inputReader.SecondaryAttackEvent -= OnSecondaryAttack;
        _inputReader.SecondaryAttackCanceledEvent -= OnSecondaryAttackCanceled;
    }

    // Update is called once per frame
    void Update() {
    }

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

    private void OnRollCanceled()
    {
        isRolling = false;
    }

    private void OnInteract()
    {
        Core.GetCoreComponent<InteractableDetector>().TryInteract();
    }

    private void OnPrimaryAttack()
    {
        attackInput[(int)CombatInputs.primary] = true;
    }

    private void OnSecondaryAttack()
    {
        attackInput[(int)CombatInputs.secondary] = true;
    }

    private void OnPrimaryAttackCanceled()
    {
        attackInput[(int)CombatInputs.primary] = false;
    }

    private void OnSecondaryAttackCanceled()
    {
        attackInput[(int)CombatInputs.secondary] = false;
    }

    public void UseAttackInput(int i) => attackInput[i] = false;
}
