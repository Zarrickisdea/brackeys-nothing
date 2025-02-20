using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(Player player) : base(player) { }

    public override void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        player.Body.linearVelocity = new Vector2(horizontalInput * player.MoveSpeed, player.Body.linearVelocity.y);

        if (horizontalInput == 0)
        {
            player.SetState<PlayerIdleState>();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.CanJump)
        {
            player.SetState<PlayerJumpState>();
            return;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            player.SetState<PlayerCrawlState>();
            return;
        }
    }
}