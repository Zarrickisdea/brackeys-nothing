public abstract class PlayerBaseState : BaseState
{
    protected Player player;

    public PlayerBaseState(Player player)
    {
        this.player = player;
    }

    public virtual void Update() { }
}