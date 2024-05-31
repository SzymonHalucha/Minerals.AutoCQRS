namespace Minerals.AutoCQRS
{
    public interface ICommandPipelineDispatcher
    {
        public Task<IReadOnlyList<TResult>> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellation = default)
            where TCommand : ICommand, new()
            where TResult : notnull;
    }
}