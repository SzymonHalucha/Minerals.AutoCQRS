namespace Minerals.AutoCQRS
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand, new()
    {
        public Task Handle(TCommand command, CancellationToken cancellation);
    }

    public interface ICommandHandler<TCommand, TResult>
        where TCommand : ICommand, new()
        where TResult : notnull
    {
        public Task<TResult> Handle(TCommand command, CancellationToken cancellation);
    }
}