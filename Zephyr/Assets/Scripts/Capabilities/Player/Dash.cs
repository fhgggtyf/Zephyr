using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : PlayerCapabilities
{
    public Dash(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void CapabilityAction()
    {
        Movement?.SetVelocityX(Movement.CurrentVelocity.x + 10);
    }
}

