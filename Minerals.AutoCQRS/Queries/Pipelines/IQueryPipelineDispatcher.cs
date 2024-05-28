namespace Minerals.AutoCQRS
{
    public interface IQueryPipelineDispatcher
    {
        public Task<IReadOnlyList<TResult>> Dispatch<TQuery, TResult>(TQuery query, CancellationToken cancellation)
            where TQuery : IQuery, new()
            where TResult : notnull;
    }
}