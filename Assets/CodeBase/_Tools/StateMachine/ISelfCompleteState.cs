namespace CodeBase._Tools.StateMachine
{
    public interface ISelfCompleteState : IState
    {
        public bool Complete { get; }
    }
}