namespace Minerals.AutoCQRS
{
    public class CommandPipelineDispatcher(IServiceProvider provider) : ICommandPipelineDispatcher
    {
        private readonly IServiceProvider _provider = provider;

        public async Task<IReadOnlyList<TResult>> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellation = default)
            where TCommand : ICommand, new()
            where TResult : notnull
        {
            List<TResult> results = [];
            IAsyncEnumerable<TResult> enumerable = _provider.GetRequiredService<ICommandPipeline<TCommand, TResult>>().Handle(command, cancellation);
            await foreach (var item in enumerable)
            {
                results.Add(item);
            }
            return results;
        }
    }
}