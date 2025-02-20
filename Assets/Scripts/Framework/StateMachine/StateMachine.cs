public abstract class StateMachine
{
    protected BaseState currentState;
    public BaseState CurrentState => currentState;

    public virtual void SetState(BaseState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.Enter();
        }
    }
}