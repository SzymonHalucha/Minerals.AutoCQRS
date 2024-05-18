namespace Minerals.AutoCQRS.Interfaces
{
    public interface ICommandDispatcher
    {
        public Task<TResult> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellation) where TCommand : ICommand, new();
    }
}