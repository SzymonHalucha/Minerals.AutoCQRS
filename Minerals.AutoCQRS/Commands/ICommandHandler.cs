namespace Minerals.AutoCQRS
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand, new()
    {
        public Task Handle(TCommand command, CancellationToken cancellation);
    }

    public interface ICommandHandler<TResult, TCommand>
        where TResult : notnull
        where TCommand : ICommand, new()
    {
        public Task<TResult> Handle(TCommand command, CancellationToken cancellation);
    }
}