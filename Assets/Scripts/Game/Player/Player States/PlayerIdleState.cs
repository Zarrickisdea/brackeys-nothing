using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(Player player) : base(player) { }

    public override void Enter()
    {
        player.IsCrawling = false;
    }

    public override void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput != 0)
        {
            player.SetState<PlayerWalkState>();
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