namespace Minerals.AutoCQRS
{
    public class CommandPipelineDispatcher(IServiceProvider provider) : ICommandPipelineDispatcher
    {
        private readonly IServiceProvider _provider = provider;

        public async Task<List<TResult>> Dispatch<TResult, TCommand>(TCommand command, CancellationToken cancellation)
            where TResult : notnull
            where TCommand : ICommand, new()
        {
            List<TResult> results = [];
            IAsyncEnumerable<TResult> enumerable = _provider.GetRequiredService<ICommandPipeline<TResult, TCommand>>().Handle(command, cancellation);
            await foreach (var item in enumerable)
            {
                results.Add(item);
            }
            return results;
        }
    }
}