namespace Minerals.AutoCQRS
{
    public interface IQueryPipelineDispatcher
    {
        public Task<IReadOnlyList<TResult>> Dispatch<TQuery, TResult>(TQuery query, CancellationToken cancellation = default)
            where TQuery : IQuery, new()
            where TResult : notnull;
    }
}