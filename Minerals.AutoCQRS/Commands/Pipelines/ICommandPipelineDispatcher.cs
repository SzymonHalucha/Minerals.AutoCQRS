namespace Minerals.AutoCQRS
{
    public interface ICommandPipelineDispatcher
    {
        public Task<List<TResult>> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellation)
            where TCommand : ICommand, new()
            where TResult : notnull;
    }
}