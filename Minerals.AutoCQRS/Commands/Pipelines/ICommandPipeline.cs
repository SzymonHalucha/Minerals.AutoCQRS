namespace Minerals.AutoCQRS
{
    public interface ICommandPipeline<TResult, TCommand>
        where TResult : notnull
        where TCommand : ICommand, new()
    {
        public IAsyncEnumerable<TResult> Handle(TCommand command, CancellationToken cancellation);
    }

    public interface ICommandPipeline<TResult, TCommand, T1, T2> : ICommandPipeline<TResult, TCommand>
        where TResult : notnull
        where TCommand : ICommand, new()
        where T1 : ICommandHandler<TResult, TCommand>, new()
        where T2 : ICommandHandler<TResult, TCommand>, new();

    public interface ICommandPipeline<TResult, TCommand, T1, T2, T3> : ICommandPipeline<TResult, TCommand>
        where TResult : notnull
        where TCommand : ICommand, new()
        where T1 : ICommandHandler<TResult, TCommand>, new()
        where T2 : ICommandHandler<TResult, TCommand>, new()
        where T3 : ICommandHandler<TResult, TCommand>, new();

    public interface ICommandPipeline<TResult, TCommand, T1, T2, T3, T4> : ICommandPipeline<TResult, TCommand>
        where TResult : notnull
        where TCommand : ICommand, new()
        where T1 : ICommandHandler<TResult, TCommand>, new()
        where T2 : ICommandHandler<TResult, TCommand>, new()
        where T3 : ICommandHandler<TResult, TCommand>, new()
        where T4 : ICommandHandler<TResult, TCommand>, new();

    public interface ICommandPipeline<TResult, TCommand, T1, T2, T3, T4, T5> : ICommandPipeline<TResult, TCommand>
        where TResult : notnull
        where TCommand : ICommand, new()
        where T1 : ICommandHandler<TResult, TCommand>, new()
        where T2 : ICommandHandler<TResult, TCommand>, new()
        where T3 : ICommandHandler<TResult, TCommand>, new()
        where T4 : ICommandHandler<TResult, TCommand>, new()
        where T5 : ICommandHandler<TResult, TCommand>, new();

    public interface ICommandPipeline<TResult, TCommand, T1, T2, T3, T4, T5, T6> : ICommandPipeline<TResult, TCommand>
        where TResult : notnull
        where TCommand : ICommand, new()
        where T1 : ICommandHandler<TResult, TCommand>, new()
        where T2 : ICommandHandler<TResult, TCommand>, new()
        where T3 : ICommandHandler<TResult, TCommand>, new()
        where T4 : ICommandHandler<TResult, TCommand>, new()
        where T5 : ICommandHandler<TResult, TCommand>, new()
        where T6 : ICommandHandler<TResult, TCommand>, new();

    public interface ICommandPipeline<TResult, TCommand, T1, T2, T3, T4, T5, T6, T7> : ICommandPipeline<TResult, TCommand>
        where TResult : notnull
        where TCommand : ICommand, new()
        where T1 : ICommandHandler<TResult, TCommand>, new()
        where T2 : ICommandHandler<TResult, TCommand>, new()
        where T3 : ICommandHandler<TResult, TCommand>, new()
        where T4 : ICommandHandler<TResult, TCommand>, new()
        where T5 : ICommandHandler<TResult, TCommand>, new()
        where T6 : ICommandHandler<TResult, TCommand>, new()
        where T7 : ICommandHandler<TResult, TCommand>, new();

    public interface ICommandPipeline<TResult, TCommand, T1, T2, T3, T4, T5, T6, T7, T8> : ICommandPipeline<TResult, TCommand>
        where TResult : notnull
        where TCommand : ICommand, new()
        where T1 : ICommandHandler<TResult, TCommand>, new()
        where T2 : ICommandHandler<TResult, TCommand>, new()
        where T3 : ICommandHandler<TResult, TCommand>, new()
        where T4 : ICommandHandler<TResult, TCommand>, new()
        where T5 : ICommandHandler<TResult, TCommand>, new()
        where T6 : ICommandHandler<TResult, TCommand>, new()
        where T7 : ICommandHandler<TResult, TCommand>, new()
        where T8 : ICommandHandler<TResult, TCommand>, new();
}