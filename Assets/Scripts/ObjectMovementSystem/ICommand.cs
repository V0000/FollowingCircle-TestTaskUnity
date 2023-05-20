namespace ObjectMovementSystem
{
    public interface ICommand
    {
        bool IsFinished { get; }
        void Execute();
    }
}