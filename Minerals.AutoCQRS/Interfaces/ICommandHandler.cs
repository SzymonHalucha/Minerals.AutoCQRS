namespace Minerals.AutoCQRS.Interfaces
{
    public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand, new()
    {
        public Task<TResult> Handle(TCommand command, CancellationToken cancellation);
    }
}