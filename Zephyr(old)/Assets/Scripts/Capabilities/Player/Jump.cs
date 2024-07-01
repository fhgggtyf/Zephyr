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
        float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * player.PlayerData.JumpHeight);
        if (player.RB.velocity.y > 0f)
        {
            jumpSpeed = Mathf.Max(jumpSpeed - player.RB.velocity.y, 0f);
        }

        Movement.SetVelocityY(jumpSpeed);
        player.InputHandler.UseJumpInput();
    }
}
