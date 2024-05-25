namespace Minerals.AutoCQRS
{
    public class TestCommand : ICommand;

    [CommandPipelineItem]
    public class TestCommandValidatorHandler : ICommandHandler<TestCommand, int>
    {
        public Task<int> Handle(TestCommand command, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }

    [CommandPipelineItem]
    public class TestCommandCacheHandler : ICommandHandler<TestCommand, int>
    {
        public Task<int> Handle(TestCommand command, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }

    [CommandPipelineItem]
    public class TestCommandExecuteHandler : ICommandHandler<TestCommand, int>
    {
        public Task<int> Handle(TestCommand command, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }

    public partial class TestCommandPipeline : ICommandPipelineHandler<TestCommand, int, TestCommandValidatorHandler, TestCommandCacheHandler, TestCommandExecuteHandler>
    {

    }

    public partial class TestCommandPipeline
    {
        public async IAsyncEnumerable<int> Handle(TestCommand command, [EnumeratorCancellation] CancellationToken cancellation)
        {
            yield return await new TestCommandValidatorHandler().Handle(command, cancellation);
            yield return await new TestCommandCacheHandler().Handle(command, cancellation);
            yield return await new TestCommandExecuteHandler().Handle(command, cancellation);
        }
    }
}