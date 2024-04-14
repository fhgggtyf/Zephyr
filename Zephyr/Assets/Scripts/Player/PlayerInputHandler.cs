using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public event Action<bool> OnInteractInputChanged;

    private PlayerInput playerInput;
    private Camera cam;

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


    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        cam = Camera.current;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnInteractInputChanged?.Invoke(true);
            return;
        }

        if (context.canceled)
        {
            OnInteractInputChanged?.Invoke(false);
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput = true;
        }

        if (context.canceled)
        {
            AttackInput = false;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);

        Debug.Log(NormInputX + " " + NormInputY);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
        }

        if (context.canceled)
        {
            JumpInput = false;
        }
    }
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }

        if (context.canceled)
        {
            GrabInput = false;
        }
    }

    public void OnCrouchInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CrouchInput = true;
        }

        if (context.canceled)
        {
            CrouchInput = false;
        }
    }


    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
        }
        else if (context.canceled)
        {
            DashInput = false;
        }
    }

    public void OnAbilityOneInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Ability1Input = true;
        }

        if (context.canceled)
        {
            Ability1Input = false;
        }
    }

    public void OnAbilityTwoInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Ability2Input = true;
        }

        if (context.canceled)
        {
            Ability2Input = false;
        }
    }

    public void OnAbilityThreeInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Ability3Input = true;
        }

        if (context.canceled)
        {
            Ability3Input = false;
        }
    }

    public void OnAbilityFourInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Ability4Input = true;
        }

        if (context.canceled)
        {
            Ability4Input = false;
        }
    }



    public void UseJumpInput() => JumpInput = false;

    public void UseDashInput() => DashInput = false;

    /// <summary>
    /// Used to set the specific attack input back to false. Usually passed through the player attack state from an animation event.
    /// </summary>
    public void UseAttackInput() => AttackInput = false;



}

