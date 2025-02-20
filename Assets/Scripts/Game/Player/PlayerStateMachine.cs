using System.Collections.Generic;

public class PlayerStateMachine : StateMachine
{
    private Dictionary<System.Type, PlayerBaseState> states;

    public PlayerStateMachine(Player player)
    {
        states = new Dictionary<System.Type, PlayerBaseState>
        {
            { typeof(PlayerIdleState), new PlayerIdleState(player) },
            { typeof(PlayerWalkState), new PlayerWalkState(player) },
            { typeof(PlayerJumpState), new PlayerJumpState(player) },
            { typeof(PlayerCrawlState), new PlayerCrawlState(player) }
        };
    }

    public void SetState<T>() where T : PlayerBaseState
    {
        SetState(states[typeof(T)]);
    }

    public void UpdateState()
    {
        if (currentState != null)
        {
            (currentState as PlayerBaseState).Update();
        }
    }
}