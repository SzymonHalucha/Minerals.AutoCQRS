namespace PipelinesTestEnvironment
{
    // Command Pipeline Handlers
    public class TestCommand1 : ICommand
    {
        public int Value { get; set; } = 1;
    }

    public class TestCommand1Handler1 : ICommandHandler<TestCommand1, string>
    {
        public Task<string> Handle(TestCommand1 command, CancellationToken cancellation = default)
        {
            return Task.FromResult($"{GetType().Name} - {command.Value}");
        }
    }

    public class TestCommand1Handler2 : ICommandHandler<TestCommand1, string>
    {
        public Task<string> Handle(TestCommand1 command, CancellationToken cancellation = default)
        {
            return Task.FromResult($"{GetType().Name} - {command.Value}");
        }
    }

    public class TestCommand1Handler3 : ICommandHandler<TestCommand1, string>
    {
        public Task<string> Handle(TestCommand1 command, CancellationToken cancellation = default)
        {
            return Task.FromResult($"{GetType().Name} - {command.Value}");
        }
    }

    public partial class TestCommand1Pipeline1 : ICommandPipeline<TestCommand1, string, TestCommand1Handler1, TestCommand1Handler2>;

    // Query Pipeline Handlers
    public class TestQuery1 : IQuery
    {
        public int Value { get; set; } = 1;
    }

    public class TestQuery1Handler1 : IQueryHandler<TestQuery1, string>
    {
        public Task<string> Handle(TestQuery1 query, CancellationToken cancellation = default)
        {
            return Task.FromResult($"{GetType().Name} - {query.Value}");
        }
    }

    public class TestQuery1Handler2 : IQueryHandler<TestQuery1, string>
    {
        public Task<string> Handle(TestQuery1 query, CancellationToken cancellation = default)
        {
            return Task.FromResult($"{GetType().Name} - {query.Value}");
        }
    }

    public class TestQuery1Handler3 : IQueryHandler<TestQuery1, string>
    {
        public Task<string> Handle(TestQuery1 query, CancellationToken cancellation = default)
        {
            return Task.FromResult($"{GetType().Name} - {query.Value}");
        }
    }
    
    public partial class TestQuery1Pipeline1 : IQueryPipeline<TestQuery1, string, TestQuery1Handler1, TestQuery1Handler2>;
}