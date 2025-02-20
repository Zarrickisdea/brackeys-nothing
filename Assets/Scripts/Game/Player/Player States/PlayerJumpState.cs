using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(Player player) : base(player) { }

    public override void Enter()
    {
        player.IsJumping = true;
        player.CanJump = false;
        player.CanDoubleJump = true;
        player.Body.linearVelocity = new Vector2(player.Body.linearVelocity.x, player.JumpHeight);
    }

    public override void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        player.Body.linearVelocity = new Vector2(horizontalInput * player.MoveSpeed, player.Body.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && player.CanDoubleJump)
        {
            player.CanDoubleJump = false;
            player.Body.linearVelocity = new Vector2(player.Body.linearVelocity.x, player.JumpHeight);
        }
    }
}