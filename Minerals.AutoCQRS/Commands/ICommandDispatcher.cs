namespace Minerals.AutoCQRS
{
    public interface ICommandDispatcher
    {
        public Task Dispatch<TCommand>(TCommand command, CancellationToken cancellation)
            where TCommand : ICommand, new();

        public Task<TResult> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellation)
            where TCommand : ICommand, new()
            where TResult : notnull;
    }
}