namespace Minerals.AutoCQRS
{
    public interface ICommandPipelineDispatcher
    {
        public Task<List<TResult>> Dispatch<TResult, TCommand>(TCommand command, CancellationToken cancellation)
            where TResult : notnull
            where TCommand : ICommand, new();
    }
}