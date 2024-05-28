namespace Minerals.AutoCQRS
{
    public interface IQueryPipeline<TQuery, TResult>
        where TQuery : IQuery, new()
        where TResult : notnull
    {
        public IAsyncEnumerable<TResult> Handle(TQuery query, CancellationToken cancellation);
    }

    public interface IQueryPipeline<TQuery, TResult, T1, T2> : IQueryPipeline<TQuery, TResult>
        where TQuery : IQuery, new()
        where TResult : notnull
        where T1 : IQueryHandler<TQuery, TResult>
        where T2 : IQueryHandler<TQuery, TResult>;

    public interface IQueryPipeline<TQuery, TResult, T1, T2, T3> : IQueryPipeline<TQuery, TResult>
        where TQuery : IQuery, new()
        where TResult : notnull
        where T1 : IQueryHandler<TQuery, TResult>
        where T2 : IQueryHandler<TQuery, TResult>
        where T3 : IQueryHandler<TQuery, TResult>;

    public interface IQueryPipeline<TQuery, TResult, T1, T2, T3, T4> : IQueryPipeline<TQuery, TResult>
        where TQuery : IQuery, new()
        where TResult : notnull
        where T1 : IQueryHandler<TQuery, TResult>
        where T2 : IQueryHandler<TQuery, TResult>
        where T3 : IQueryHandler<TQuery, TResult>
        where T4 : IQueryHandler<TQuery, TResult>;

    public interface IQueryPipeline<TQuery, TResult, T1, T2, T3, T4, T5> : IQueryPipeline<TQuery, TResult>
        where TQuery : IQuery, new()
        where TResult : notnull
        where T1 : IQueryHandler<TQuery, TResult>
        where T2 : IQueryHandler<TQuery, TResult>
        where T3 : IQueryHandler<TQuery, TResult>
        where T4 : IQueryHandler<TQuery, TResult>
        where T5 : IQueryHandler<TQuery, TResult>;

    public interface IQueryPipeline<TQuery, TResult, T1, T2, T3, T4, T5, T6> : IQueryPipeline<TQuery, TResult>
        where TQuery : IQuery, new()
        where TResult : notnull
        where T1 : IQueryHandler<TQuery, TResult>
        where T2 : IQueryHandler<TQuery, TResult>
        where T3 : IQueryHandler<TQuery, TResult>
        where T4 : IQueryHandler<TQuery, TResult>
        where T5 : IQueryHandler<TQuery, TResult>
        where T6 : IQueryHandler<TQuery, TResult>;

    public interface IQueryPipeline<TQuery, TResult, T1, T2, T3, T4, T5, T6, T7> : IQueryPipeline<TQuery, TResult>
        where TQuery : IQuery, new()
        where TResult : notnull
        where T1 : IQueryHandler<TQuery, TResult>
        where T2 : IQueryHandler<TQuery, TResult>
        where T3 : IQueryHandler<TQuery, TResult>
        where T4 : IQueryHandler<TQuery, TResult>
        where T5 : IQueryHandler<TQuery, TResult>
        where T6 : IQueryHandler<TQuery, TResult>
        where T7 : IQueryHandler<TQuery, TResult>;

    public interface IQueryPipeline<TQuery, TResult, T1, T2, T3, T4, T5, T6, T7, T8> : IQueryPipeline<TQuery, TResult>
        where TQuery : IQuery, new()
        where TResult : notnull
        where T1 : IQueryHandler<TQuery, TResult>
        where T2 : IQueryHandler<TQuery, TResult>
        where T3 : IQueryHandler<TQuery, TResult>
        where T4 : IQueryHandler<TQuery, TResult>
        where T5 : IQueryHandler<TQuery, TResult>
        where T6 : IQueryHandler<TQuery, TResult>
        where T7 : IQueryHandler<TQuery, TResult>
        where T8 : IQueryHandler<TQuery, TResult>;
}