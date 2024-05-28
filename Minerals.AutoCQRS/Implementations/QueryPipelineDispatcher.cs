namespace Minerals.AutoCQRS
{
    public class QueryPipelineDispatcher(IServiceProvider provider) : IQueryPipelineDispatcher
    {
        private readonly IServiceProvider _provider = provider;

        public async Task<IReadOnlyList<TResult>> Dispatch<TQuery, TResult>(TQuery query, CancellationToken cancellation)
            where TQuery : IQuery, new()
            where TResult : notnull
        {
            List<TResult> results = [];
            IAsyncEnumerable<TResult> enumerable = _provider.GetRequiredService<IQueryPipeline<TQuery, TResult>>().Handle(query, cancellation);
            await foreach (var item in enumerable)
            {
                results.Add(item);
            }
            return results;
        }
    }
}