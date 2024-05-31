namespace Minerals.AutoCQRS
{
    public class QueryDispatcher(IServiceProvider provider) : IQueryDispatcher
    {
        private readonly IServiceProvider _provider = provider;

        public Task<TResult> Dispatch<TQuery, TResult>(TQuery query, CancellationToken cancellation = default)
            where TQuery : IQuery, new()
            where TResult : notnull
        {
            return _provider.GetRequiredService<IQueryHandler<TQuery, TResult>>().Handle(query, cancellation);
        }
    }
}