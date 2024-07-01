using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : PlayerCapabilities
{
    public Roll(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    public override void CapabilityAction()
    {
        Movement?.SetVelocityX(Movement.CurrentVelocity.x + 10);
    }
}
