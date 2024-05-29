namespace Minerals.AutoCQRS.Tests
{
    [TestClass]
    public class IServiceCollectionExtensionsGeneratorTests : VerifyBase
    {
        public IServiceCollectionExtensionsGeneratorTests()
        {
            var references = VerifyExtensions.GetAppReferences
            (
                typeof(object),
                typeof(CodeBuilder),
                typeof(StringCases.StringExtensions),
                typeof(ICommand),
                typeof(IServiceCollectionExtensionsGenerator),
                typeof(Assembly)
            );
            VerifyExtensions.Initialize(references);
        }

        [TestMethod]
        public Task Empty_ShouldGenerate()
        {
            const string source = """
            namespace Examples
            {
                public class ExampleClass
                {

                }
            }
            """;
            return this.VerifyIncrementalGenerators(source, new IServiceCollectionExtensionsGenerator());
        }

        [TestMethod]
        public Task CommandHandler_ShouldGenerate()
        {
            const string source = """
            namespace Examples
            {
                public class ExampleCommand : Minerals.AutoCQRS.ICommand
                {

                }

                public class ExampleCommandHandler : Minerals.AutoCQRS.ICommandHandler<ExampleCommand, int>
                {
                    public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                    {
                        return Task.FromResult<int>(default!);
                    }
                }
            }
            """;
            return this.VerifyIncrementalGenerators(source, new IServiceCollectionExtensionsGenerator());
        }

        [TestMethod]
        public Task MultipleCommandHandler_ShouldGenerate()
        {
            const string source = """
            namespace Examples
            {
                public class ExampleCommand : Minerals.AutoCQRS.ICommand
                {

                }

                public class ExampleCommandHandler1 : Minerals.AutoCQRS.ICommandHandler<ExampleCommand, int>
                {
                    public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                    {
                        return Task.FromResult<int>(default!);
                    }
                }
            }

            namespace TestNamespace2
            {
                using Examples;

                public class ExampleCommandHandler2 : Minerals.AutoCQRS.ICommandHandler<ExampleCommand, int>
                {
                    public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                    {
                        return Task.FromResult<int>(default!);
                    }
                }
            }

            namespace TestNamespace3
            {
                using Examples;

                public class ExampleCommandHandler3 : Minerals.AutoCQRS.ICommandHandler<ExampleCommand, int>
                {
                    public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                    {
                        return Task.FromResult<int>(default!);
                    }
                }
            }
            """;
            return this.VerifyIncrementalGenerators(source, new IServiceCollectionExtensionsGenerator());
        }

        [TestMethod]
        public Task CommandAndQueryHandler_ShouldGenerate()
        {
            const string source = """
            namespace Examples0
            {
                public class ExampleCommand : Minerals.AutoCQRS.ICommand
                {

                }

                public class ExampleCommandHandler : Minerals.AutoCQRS.ICommandHandler<ExampleCommand, int>
                {
                    public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                    {
                        return Task.FromResult<int>(default!);
                    }
                }
            }

            namespace Examples1
            {
                public class ExampleQuery : Minerals.AutoCQRS.IQuery
                {

                }

                public class ExampleQueryHandler : Minerals.AutoCQRS.IQueryHandler<ExampleQuery, int>
                {
                    public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                    {
                        return Task.FromResult<int>(default!);
                    }
                }
            }
            """;
            return this.VerifyIncrementalGenerators(source, new IServiceCollectionExtensionsGenerator());
        }

        [TestMethod]
        public Task QueryHandler_ShouldGenerate()
        {
            const string source = """
            namespace Examples
            {
                public class ExampleQuery : Minerals.AutoCQRS.IQuery
                {

                }

                public class ExampleQueryHandler : Minerals.AutoCQRS.IQueryHandler<ExampleQuery, int>
                {
                    public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                    {
                        return Task.FromResult<int>(default!);
                    }
                }
            }
            """;
            return this.VerifyIncrementalGenerators(source, new IServiceCollectionExtensionsGenerator());
        }

        [TestMethod]
        public Task CommandPipelineTwoHandlers_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoCQRS;

            namespace Examples
            {
                public class ExampleCommand : ICommand;

                public class ExampleCommandHandler1 : ICommandHandler<ExampleCommand, int>
                {
                    public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                    {
                        return Task.FromResult<int>(default!);
                    }
                }

                public class ExampleCommandHandler2 : ICommandHandler<ExampleCommand, int>
                {
                    public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                    {
                        return Task.FromResult<int>(default!);
                    }
                }

                public partial class ExampleCommandPipeline : ICommandPipeline<ExampleCommand, int, ExampleCommandHandler1, ExampleCommandHandler2>;
            }
            """;
            return this.VerifyIncrementalGenerators(source, new IServiceCollectionExtensionsGenerator(), [new PipelinesGenerator()]);
        }

        [TestMethod]
        public Task QueryPipelineTwoHandlers_ShouldGenerate()
        {
            const string source = """
            using Minerals.AutoCQRS;

            namespace Examples
            {
                public class ExampleQuery : IQuery;

                public class ExampleQueryHandler1 : IQueryHandler<ExampleQuery, int>
                {
                    public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                    {
                        return Task.FromResult<int>(default!);
                    }
                }

                public class ExampleQueryHandler2 : IQueryHandler<ExampleQuery, int>
                {
                    public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                    {
                        return Task.FromResult<int>(default!);
                    }
                }

                public partial class ExampleQueryPipeline : IQueryPipeline<ExampleQuery, int, ExampleQueryHandler1, ExampleQueryHandler2>;
            }
            """;
            return this.VerifyIncrementalGenerators(source, new IServiceCollectionExtensionsGenerator(), [new PipelinesGenerator()]);
        }
    }
}