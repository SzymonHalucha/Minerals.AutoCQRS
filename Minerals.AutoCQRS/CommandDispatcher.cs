namespace Minerals.AutoCQRS
{
    public partial class CommandDispatcher(IServiceProvider provider) : ICommandDispatcher
    {
        private readonly IServiceProvider _services = provider;

        public Task<TResult> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellation) where TCommand : ICommand, new()
        {
            return _services.GetRequiredService<ICommandHandler<TCommand, TResult>>().Handle(command, cancellation);
        }
    }
}