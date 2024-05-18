namespace Minerals.AutoCQRS
{
    public class QueryDispatcher(IServiceProvider provider) : IQueryDispatcher
    {
        private readonly IServiceProvider _services = provider;

        public Task<TResult> Dispatch<TQuery, TResult>(TQuery query, CancellationToken cancellation) where TQuery : IQuery, new()
        {
            return _services.GetRequiredService<IQueryHandler<TQuery, TResult>>().Handle(query, cancellation);
        }
    }
}