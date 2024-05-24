namespace Minerals.AutoCQRS.Interfaces
{
    public partial interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery, new()
    {
        public Task<TResult> Handle(TQuery query, CancellationToken cancellation);
    }
}