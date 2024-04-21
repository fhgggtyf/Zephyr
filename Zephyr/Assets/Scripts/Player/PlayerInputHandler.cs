using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    private PlayerInput _playerInput;
    //private InputAction 

    public event Action<bool> OnInteractInputChanged;

    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool CrouchInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool GrabInput { get; private set; }

    public bool AttackInput { get; private set; }
    public bool Ability1Input { get; private set; }
    public bool Ability2Input { get; private set; }
    public bool Ability3Input { get; private set; }
    public bool Ability4Input { get; private set; }


    void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        _playerInputActions.GamePlay.Interaction.performed += OnInteractInput;
        _playerInputActions.GamePlay.Interaction.canceled += OnInteractInputCanceled;
        _playerInputActions.GamePlay.Interaction.Enable();
        _playerInputActions.GamePlay.Attack.performed += OnAttackInput;
        _playerInputActions.GamePlay.Attack.canceled += OnAttackInputCanceled;
        _playerInputActions.GamePlay.Attack.Enable();
        _playerInputActions.GamePlay.Movement.performed += OnMoveInput;
        _playerInputActions.GamePlay.Movement.canceled += OnMoveInputCanceled;
        _playerInputActions.GamePlay.Movement.Enable();
        _playerInputActions.GamePlay.Jump.performed += OnJumpInput;
        _playerInputActions.GamePlay.Jump.canceled += OnJumpInputCanceled;
        _playerInputActions.GamePlay.Jump.Enable();
        //_playerInput.GamePlay.Grab.performed += OnGrabInput;
        //_playerInput.GamePlay.Grab.canceled += OnGrabInputCanceled;
        //_playerInput.GamePlay.Grab.Enable();
        _playerInputActions.GamePlay.Crouch.performed += OnCrouchInput;
        _playerInputActions.GamePlay.Crouch.canceled += OnCrouchInputCanceled;
        _playerInputActions.GamePlay.Crouch.Enable();
        _playerInputActions.GamePlay.Dash.performed += OnDashInput;
        _playerInputActions.GamePlay.Dash.canceled += OnDashInputCanceled;
        _playerInputActions.GamePlay.Dash.Enable();
        _playerInputActions.GamePlay.Ability1.performed += OnAbilityOneInput;
        _playerInputActions.GamePlay.Ability1.canceled += OnAbilityOneInputCanceled;
        _playerInputActions.GamePlay.Ability1.Enable();
        _playerInputActions.GamePlay.Ability2.performed += OnAbilityTwoInput;
        _playerInputActions.GamePlay.Ability2.canceled += OnAbilityTwoInputCanceled;
        _playerInputActions.GamePlay.Ability2.Enable();
        _playerInputActions.GamePlay.Ability3.performed += OnAbilityThreeInput;
        _playerInputActions.GamePlay.Ability3.canceled += OnAbilityThreeInputCanceled;
        _playerInputActions.GamePlay.Ability3.Enable();
        _playerInputActions.GamePlay.Ability4.performed += OnAbilityFourInput;
        _playerInputActions.GamePlay.Ability4.canceled += OnAbilityFourInputCanceled;
        _playerInputActions.GamePlay.Ability4.Enable();


    }

    private void OnDisable()
    {

        _playerInputActions.GamePlay.Interaction.Disable();

        _playerInputActions.GamePlay.Attack.Disable();

        _playerInputActions.GamePlay.Movement.Disable();

        _playerInputActions.GamePlay.Jump.Disable();

        //_playerInput.GamePlay.Grab.Disable();

        _playerInputActions.GamePlay.Crouch.Disable();

        _playerInputActions.GamePlay.Dash.Disable();

        _playerInputActions.GamePlay.Ability1.Disable();

        _playerInputActions.GamePlay.Ability2.Disable();

        _playerInputActions.GamePlay.Ability3.Disable();

        _playerInputActions.GamePlay.Ability4.Disable();


    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        OnInteractInputChanged?.Invoke(true);
        return;
    }
    public void OnInteractInputCanceled(InputAction.CallbackContext context)
    {
        OnInteractInputChanged?.Invoke(false);
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }
    public void OnMoveInputCanceled(InputAction.CallbackContext context)
    {
        RawMovementInput = Vector2.zero;

        NormInputX = 0;
        NormInputY = 0;
    }

    public void OnAttackInput(InputAction.CallbackContext context) => AttackInput = true;
    public void OnAttackInputCanceled(InputAction.CallbackContext context) => AttackInput = false;
    public void OnJumpInput(InputAction.CallbackContext context) => JumpInput = true;
    public void OnJumpInputCanceled(InputAction.CallbackContext context) => JumpInput = false;
    public void OnGrabInput(InputAction.CallbackContext context) => GrabInput = true;
    public void OnGrabInputCanceled(InputAction.CallbackContext context) => GrabInput = false;
    public void OnCrouchInput(InputAction.CallbackContext context) => CrouchInput = true;
    public void OnCrouchInputCanceled(InputAction.CallbackContext context) => CrouchInput = false;
    public void OnDashInput(InputAction.CallbackContext context) => DashInput = true;
    public void OnDashInputCanceled(InputAction.CallbackContext context) => DashInput = false;
    public void OnAbilityOneInput(InputAction.CallbackContext context) => Ability1Input = true;
    public void OnAbilityOneInputCanceled(InputAction.CallbackContext context) => Ability1Input = false;
    public void OnAbilityTwoInput(InputAction.CallbackContext context) => Ability2Input = true;
    public void OnAbilityTwoInputCanceled(InputAction.CallbackContext context) => Ability2Input = false;
    public void OnAbilityThreeInput(InputAction.CallbackContext context) => Ability3Input = true;
    public void OnAbilityThreeInputCanceled(InputAction.CallbackContext context) => Ability3Input = false;
    public void OnAbilityFourInput(InputAction.CallbackContext context) => Ability4Input = true;
    public void OnAbilityFourInputCanceled(InputAction.CallbackContext context) => Ability4Input = false;

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;

    /// <summary>
    /// Used to set the specific attack input back to false. Usually passed through the player attack state from an animation event.
    /// </summary>
    public void UseAttackInput() => AttackInput = false;



}

