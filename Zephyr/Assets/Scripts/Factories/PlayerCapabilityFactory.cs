using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerCapabilityFactory", menuName = "Capability Factory/Player")]
public class PlayerCapabilityFactory : CapabilityFactory<PlayerCapabilities>
{
    public Jump GetJump(Player player, string animboolname)
    {
        return new Jump(player, animboolname);
    }

    public Roll GetRoll(Player player, string animboolname)
    {
        return new Roll(player, animboolname);
    }

    public Dash GetDash(Player player, string animboolname)
    {
        return new Dash(player, animboolname);
    }
}
