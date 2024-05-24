namespace Minerals.AutoCQRS
{
    public partial interface ICommandDispatcher
    {
        public Task<TResult> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellation) where TCommand : ICommand, new();
    }
}