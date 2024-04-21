using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerStateMachineFactory", menuName = "State Machine Factory/Player")]
public class PlayerStateMachineFactory : StateMachineFactory<PlayerStateMachine, Player, PlayerData, Core>
{
    public override PlayerStateMachine CreateStateMachine(Player player, PlayerData data, Core core)
    {
        return new PlayerStateMachine(player, data, core);
    }
}