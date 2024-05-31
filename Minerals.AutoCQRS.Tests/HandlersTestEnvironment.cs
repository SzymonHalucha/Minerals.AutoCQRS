namespace HandlersTestEnvironment
{
    public class TestCommand1 : ICommand
    {
        public int Value { get; set; } = 0;
    }

    public class TestCommand1Handler1 : ICommandHandler<TestCommand1, string>
    {
        public Task<string> Handle(TestCommand1 command, CancellationToken cancellation)
        {
            command.Value++;
            return Task.FromResult($"{GetType().Name} - {command.Value}");
        }
    }

    public class TestCommand1Handler2 : ICommandHandler<TestCommand1, int>
    {
        public Task<int> Handle(TestCommand1 command, CancellationToken cancellation)
        {
            command.Value++;
            return Task.FromResult(command.Value++);
        }
    }

    public class TestCommand2 : ICommand
    {
        public int Value { get; set; } = 0;
    }

    public class TestCommand2Handler1 : ICommandHandler<TestCommand2, string>
    {
        public Task<string> Handle(TestCommand2 command, CancellationToken cancellation)
        {
            command.Value++;
            return Task.FromResult($"{GetType().Name} - {command.Value}");
        }
    }

    public class TestCommand2Handler2 : ICommandHandler<TestCommand2, string>
    {
        public Task<string> Handle(TestCommand2 command, CancellationToken cancellation)
        {
            command.Value++;
            return Task.FromResult($"{GetType().Name} - {command.Value}");
        }
    }

    public class TestQuery1 : IQuery
    {
        public int Value { get; set; } = 0;
    }

    public class TestQuery1Handler1 : IQueryHandler<TestQuery1, string>
    {
        public Task<string> Handle(TestQuery1 query, CancellationToken cancellation)
        {
            query.Value++;
            return Task.FromResult($"{GetType().Name} - {query.Value}");
        }
    }

    public class TestQuery1Handler2 : IQueryHandler<TestQuery1, int>
    {
        public Task<int> Handle(TestQuery1 query, CancellationToken cancellation)
        {
            query.Value++;
            return Task.FromResult(query.Value);
        }
    }

    public class TestQuery2 : IQuery
    {
        public int Value { get; set; } = 0;
    }

    public class TestQuery2Handler1 : IQueryHandler<TestQuery2, string>
    {
        public Task<string> Handle(TestQuery2 query, CancellationToken cancellation)
        {
            query.Value++;
            return Task.FromResult($"{GetType().Name} - {query.Value}");
        }
    }

    public class TestQuery2Handler2 : IQueryHandler<TestQuery2, string>
    {
        public Task<string> Handle(TestQuery2 query, CancellationToken cancellation)
        {
            query.Value++;
            return Task.FromResult($"{GetType().Name} - {query.Value}");
        }
    }
}