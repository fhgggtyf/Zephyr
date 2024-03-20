using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]

public class AIController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return true;
    }

    public override float RetrieveMoveInput()
    {
        return 1f;
    }

    public override bool RetrieveRollInput()
    {
        return false;
    }

    public override bool RetrieveRunInput()
    {
        throw new System.NotImplementedException();
    }

    public override bool RetrieveShiftInput()
    {
        throw new System.NotImplementedException();
    }
}
