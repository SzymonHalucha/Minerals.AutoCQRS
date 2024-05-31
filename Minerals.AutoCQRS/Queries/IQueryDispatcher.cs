namespace Minerals.AutoCQRS
{
    public interface IQueryDispatcher
    {
        public Task<TResult> Dispatch<TQuery, TResult>(TQuery query, CancellationToken cancellation = default)
            where TQuery : IQuery, new()
            where TResult : notnull;
    }
}