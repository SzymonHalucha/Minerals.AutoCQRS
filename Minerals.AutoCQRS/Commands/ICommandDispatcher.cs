namespace Minerals.AutoCQRS
{
    public interface ICommandDispatcher
    {
        public Task Dispatch<TCommand>(TCommand command, CancellationToken cancellation)
            where TCommand : ICommand, new();

        public Task<TResult> Dispatch<TResult, TCommand>(TCommand command, CancellationToken cancellation)
            where TResult : notnull
            where TCommand : ICommand, new();
    }
}