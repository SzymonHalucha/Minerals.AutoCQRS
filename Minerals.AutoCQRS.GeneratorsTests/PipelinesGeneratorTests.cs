namespace Minerals.AutoCQRS.GeneratorsTests
{
    [TestClass]
    public class PipelinesGeneratorTests : VerifyBase
    {
        public PipelinesGeneratorTests()
        {
            var references = VerifyExtensions.GetAppReferences
            (
                typeof(object),
                typeof(CodeBuilder),
                typeof(StringCases.StringExtensions),
                typeof(ICommand),
                typeof(PipelinesGenerator),
                typeof(Assembly)
            );
            VerifyExtensions.Initialize(references);
        }

        [TestMethod]
        public Task OneCommandThreeHandlers_ShouldGenerate()
        {
            const string source = """
            using System.Threading;
            using Minerals.AutoCQRS;

            namespace Examples
            {
                public class TestCommand : ICommand;

                public class TestCommandHandler1 : ICommandHandler<TestCommand, int>
                {
                    public Task<int> Handle(TestCommand command, CancellationToken cancellation)
                    {
                        throw new NotImplementedException();
                    }
                }

                public class TestCommandHandler2 : ICommandHandler<TestCommand, int>
                {
                    public Task<int> Handle(TestCommand command, CancellationToken cancellation)
                    {
                        throw new NotImplementedException();
                    }
                }

                public class TestCommandHandler3 : ICommandHandler<TestCommand, int>
                {
                    public Task<int> Handle(TestCommand command, CancellationToken cancellation)
                    {
                        throw new NotImplementedException();
                    }
                }

                public partial class TestCommandPipeline : ICommandPipeline<TestCommand, int, TestCommandHandler1, TestCommandHandler2, TestCommandHandler3>;
            }
            """;
            return this.VerifyIncrementalGenerators(source, new PipelinesGenerator());
        }

        [TestMethod]
        public Task OneCommandThreeHandlersWithArguments_ShouldGenerate()
        {
            const string source = """
            using System.Threading;
            using Minerals.AutoCQRS;

            namespace Examples
            {
                public class ExampleSevice1;
                public class ExampleSevice2;
                public class ExampleSevice3;
                public class TestCommand : ICommand;

                public class TestCommandHandler1 : ICommandHandler<TestCommand, int>
                {
                    public TestCommandHandler1(ExampleSevice1 sevice1)
                    {

                    }

                    public Task<int> Handle(TestCommand command, CancellationToken cancellation)
                    {
                        throw new NotImplementedException();
                    }
                }

                public class TestCommandHandler2 : ICommandHandler<TestCommand, int>
                {
                    public TestCommandHandler2(ExampleSevice2 sevice2)
                    {

                    }

                    public Task<int> Handle(TestCommand command, CancellationToken cancellation)
                    {
                        throw new NotImplementedException();
                    }
                }

                public class TestCommandHandler3 : ICommandHandler<TestCommand, int>
                {
                    public TestCommandHandler3(ExampleSevice2 sevice2, ExampleSevice3 sevice3)
                    {

                    }

                    public Task<int> Handle(TestCommand command, CancellationToken cancellation)
                    {
                        throw new NotImplementedException();
                    }
                }

                public partial class TestCommandPipeline : ICommandPipeline<TestCommand, int, TestCommandHandler1, TestCommandHandler2, TestCommandHandler3>;
            }
            """;
            return this.VerifyIncrementalGenerators(source, new PipelinesGenerator());
        }

        [TestMethod]
        public Task OneQueryThreeHandlers_ShouldGenerate()
        {
            const string source = """
            using System.Threading;
            using Minerals.AutoCQRS;

            namespace Examples
            {
                public class TestQuery : IQuery;

                public class TestQueryHandler1 : IQueryHandler<TestQuery, int>
                {
                    public Task<int> Handle(TestQuery query, CancellationToken cancellation)
                    {
                        throw new NotImplementedException();
                    }
                }

                public class TestQueryHandler2 : IQueryHandler<TestQuery, int>
                {
                    public Task<int> Handle(TestQuery query, CancellationToken cancellation)
                    {
                        throw new NotImplementedException();
                    }
                }

                public class TestQueryHandler3 : IQueryHandler<TestQuery, int>
                {
                    public Task<int> Handle(TestQuery query, CancellationToken cancellation)
                    {
                        throw new NotImplementedException();
                    }
                }

                public partial class TestQueryPipeline : IQueryPipeline<TestQuery, int, TestQueryHandler1, TestQueryHandler2, TestQueryHandler3>;
            }
            """;
            return this.VerifyIncrementalGenerators(source, new PipelinesGenerator());
        }

        [TestMethod]
        public Task OneQueryThreeHandlersWithArguments_ShouldGenerate()
        {
            const string source = """
            using System.Threading;
            using Minerals.AutoCQRS;

            namespace Examples
            {
                public class ExampleSevice1;
                public class ExampleSevice2;
                public class ExampleSevice3;
                public class TestQuery : IQuery;

                public class TestQueryHandler1 : IQueryHandler<TestQuery, int>
                {
                    public TestQueryHandler1(ExampleSevice1 sevice1)
                    {

                    }

                    public Task<int> Handle(TestQuery query, CancellationToken cancellation)
                    {
                        throw new NotImplementedException();
                    }
                }

                public class TestQueryHandler2 : IQueryHandler<TestQuery, int>
                {
                    public TestQueryHandler2(ExampleSevice2 sevice2)
                    {

                    }

                    public Task<int> Handle(TestQuery query, CancellationToken cancellation)
                    {
                        throw new NotImplementedException();
                    }
                }

                public class TestQueryHandler3 : IQueryHandler<TestQuery, int>
                {
                    public TestQueryHandler3(ExampleSevice2 sevice2, ExampleSevice3 sevice3)
                    {

                    }

                    public Task<int> Handle(TestQuery query, CancellationToken cancellation)
                    {
                        throw new NotImplementedException();
                    }
                }

                public partial class TestQueryPipeline : IQueryPipeline<TestQuery, int, TestQueryHandler1, TestQueryHandler2, TestQueryHandler3>;
            }
            """;
            return this.VerifyIncrementalGenerators(source, new PipelinesGenerator());
        }
    }
}