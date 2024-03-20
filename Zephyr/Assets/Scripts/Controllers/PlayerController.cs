using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]


public class PlayerController : InputController
{

    public override bool RetrieveJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public override float RetrieveMoveInput()
    {
        return Input.GetAxis("Horizontal");
    }

    public override bool RetrieveRollInput()
    {
        return Input.GetButtonDown("Roll/Dash");
    }

    public override bool RetrieveRunInput()
    {
        return Input.GetButtonDown("Horizontal");
    }

    public override bool RetrieveShiftInput()
    {
        return Input.GetButton("Shift");
    }
}
