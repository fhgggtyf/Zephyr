using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : PlayerCapabilities
{
    public Jump(Player player, string animBoolName) : base(player, animBoolName)
    {
    }

    // Start is called before the first frame update
    public override void CapabilityAction()
    {
        Movement?.SetVelocityY(player.PlayerData.jumpVelocity);
    }
}
