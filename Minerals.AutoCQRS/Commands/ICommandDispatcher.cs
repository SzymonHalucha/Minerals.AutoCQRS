namespace Minerals.AutoCQRS
{
    public interface ICommandDispatcher
    {
        public Task<TResult> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellation = default)
            where TCommand : ICommand, new()
            where TResult : notnull;
    }
}