using UnityEngine;

public class PlayerCrawlState : PlayerBaseState
{
    public PlayerCrawlState(Player player) : base(player) { }

    public override void Enter()
    {
        player.IsCrawling = true;
    }

    public override void Exit()
    {
        player.IsCrawling = false;
    }

    public override void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        player.Body.linearVelocity = new Vector2(horizontalInput * player.MoveSpeed * 0.5f, player.Body.linearVelocity.y);

        if (!Input.GetKey(KeyCode.DownArrow))
        {
            player.SetState<PlayerIdleState>();
            return;
        }
    }
}