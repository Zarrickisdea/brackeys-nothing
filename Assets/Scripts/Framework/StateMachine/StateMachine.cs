public abstract class StateMachine
{
    protected BaseState currentState;
    public abstract void SetState(BaseState newState, float transitionTime = 0);
}
