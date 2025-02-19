public class PlayerBaseState : BaseState
{
    protected Player playerController;

    public PlayerBaseState(Player playerController)
    {
        this.playerController = playerController;
    }

    public override void Enter() { }

    public override void Exit() { }
}
