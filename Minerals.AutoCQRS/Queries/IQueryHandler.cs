namespace Minerals.AutoCQRS
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery, new()
        where TResult : notnull
    {
        public Task<TResult> Handle(TQuery query, CancellationToken cancellation);
    }
}