namespace Minerals.AutoCQRS
{
    public interface ICommandPipeline<TCommand, TResult>
        where TCommand : ICommand, new()
        where TResult : notnull
    {
        public IAsyncEnumerable<TResult> Handle(TCommand command, CancellationToken cancellation);
    }

    public interface ICommandPipeline<TCommand, TResult, T1, T2> : ICommandPipeline<TCommand, TResult>
        where TCommand : ICommand, new()
        where TResult : notnull
        where T1 : ICommandHandler<TCommand, TResult>, new()
        where T2 : ICommandHandler<TCommand, TResult>, new();

    public interface ICommandPipeline<TCommand, TResult, T1, T2, T3> : ICommandPipeline<TCommand, TResult>
        where TCommand : ICommand, new()
        where TResult : notnull
        where T1 : ICommandHandler<TCommand, TResult>, new()
        where T2 : ICommandHandler<TCommand, TResult>, new()
        where T3 : ICommandHandler<TCommand, TResult>, new();

    public interface ICommandPipeline<TCommand, TResult, T1, T2, T3, T4> : ICommandPipeline<TCommand, TResult>
        where TCommand : ICommand, new()
        where TResult : notnull
        where T1 : ICommandHandler<TCommand, TResult>, new()
        where T2 : ICommandHandler<TCommand, TResult>, new()
        where T3 : ICommandHandler<TCommand, TResult>, new()
        where T4 : ICommandHandler<TCommand, TResult>, new();

    public interface ICommandPipeline<TCommand, TResult, T1, T2, T3, T4, T5> : ICommandPipeline<TCommand, TResult>
        where TCommand : ICommand, new()
        where TResult : notnull
        where T1 : ICommandHandler<TCommand, TResult>, new()
        where T2 : ICommandHandler<TCommand, TResult>, new()
        where T3 : ICommandHandler<TCommand, TResult>, new()
        where T4 : ICommandHandler<TCommand, TResult>, new()
        where T5 : ICommandHandler<TCommand, TResult>, new();

    public interface ICommandPipeline<TCommand, TResult, T1, T2, T3, T4, T5, T6> : ICommandPipeline<TCommand, TResult>
        where TCommand : ICommand, new()
        where TResult : notnull
        where T1 : ICommandHandler<TCommand, TResult>, new()
        where T2 : ICommandHandler<TCommand, TResult>, new()
        where T3 : ICommandHandler<TCommand, TResult>, new()
        where T4 : ICommandHandler<TCommand, TResult>, new()
        where T5 : ICommandHandler<TCommand, TResult>, new()
        where T6 : ICommandHandler<TCommand, TResult>, new();

    public interface ICommandPipeline<TCommand, TResult, T1, T2, T3, T4, T5, T6, T7> : ICommandPipeline<TCommand, TResult>
        where TCommand : ICommand, new()
        where TResult : notnull
        where T1 : ICommandHandler<TCommand, TResult>, new()
        where T2 : ICommandHandler<TCommand, TResult>, new()
        where T3 : ICommandHandler<TCommand, TResult>, new()
        where T4 : ICommandHandler<TCommand, TResult>, new()
        where T5 : ICommandHandler<TCommand, TResult>, new()
        where T6 : ICommandHandler<TCommand, TResult>, new()
        where T7 : ICommandHandler<TCommand, TResult>, new();

    public interface ICommandPipeline<TCommand, TResult, T1, T2, T3, T4, T5, T6, T7, T8> : ICommandPipeline<TCommand, TResult>
        where TCommand : ICommand, new()
        where TResult : notnull
        where T1 : ICommandHandler<TCommand, TResult>, new()
        where T2 : ICommandHandler<TCommand, TResult>, new()
        where T3 : ICommandHandler<TCommand, TResult>, new()
        where T4 : ICommandHandler<TCommand, TResult>, new()
        where T5 : ICommandHandler<TCommand, TResult>, new()
        where T6 : ICommandHandler<TCommand, TResult>, new()
        where T7 : ICommandHandler<TCommand, TResult>, new()
        where T8 : ICommandHandler<TCommand, TResult>, new();
}