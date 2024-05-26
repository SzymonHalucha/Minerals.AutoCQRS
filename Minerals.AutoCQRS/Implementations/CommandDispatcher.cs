namespace Minerals.AutoCQRS
{
    public class CommandDispatcher(IServiceProvider provider) : ICommandDispatcher
    {
        private readonly IServiceProvider _provider = provider;

        public Task Dispatch<TCommand>(TCommand command, CancellationToken cancellation)
            where TCommand : ICommand, new()
        {
            return _provider.GetRequiredService<ICommandHandler<TCommand>>().Handle(command, cancellation);
        }

        public Task<TResult> Dispatch<TResult, TCommand>(TCommand command, CancellationToken cancellation)
            where TResult : notnull
            where TCommand : ICommand, new()
        {
            return _provider.GetRequiredService<ICommandHandler<TResult, TCommand>>().Handle(command, cancellation);
        }
    }
}