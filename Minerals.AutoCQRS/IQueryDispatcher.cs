namespace Minerals.AutoCQRS
{
    public partial interface IQueryDispatcher
    {
        public Task<TResult> Dispatch<TQuery, TResult>(TQuery query, CancellationToken cancellation) where TQuery : IQuery, new();
    }
}