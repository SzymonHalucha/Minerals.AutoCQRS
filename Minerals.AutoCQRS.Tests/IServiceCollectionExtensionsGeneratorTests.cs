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
                typeof(ICommandHandler<,>),
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
                public class ExampleCommand : Minerals.AutoCQRS.Interfaces.ICommand
                {

                }

                public class ExampleCommandHandler : Minerals.AutoCQRS.Interfaces.ICommandHandler<ExampleCommand, int>
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
                public class ExampleCommand : Minerals.AutoCQRS.Interfaces.ICommand
                {

                }

                public class ExampleCommandHandler1 : Minerals.AutoCQRS.Interfaces.ICommandHandler<ExampleCommand, int>
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

                public class ExampleCommandHandler2 : Minerals.AutoCQRS.Interfaces.ICommandHandler<ExampleCommand, int>
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

                public class ExampleCommandHandler3 : Minerals.AutoCQRS.Interfaces.ICommandHandler<ExampleCommand, int>
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
                public class ExampleCommand : Minerals.AutoCQRS.Interfaces.ICommand
                {

                }

                public class ExampleCommandHandler : Minerals.AutoCQRS.Interfaces.ICommandHandler<ExampleCommand, int>
                {
                    public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                    {
                        return Task.FromResult<int>(default!);
                    }
                }
            }

            namespace Examples1
            {
                public class ExampleQuery : Minerals.AutoCQRS.Interfaces.IQuery
                {

                }

                public class ExampleQueryHandler : Minerals.AutoCQRS.Interfaces.IQueryHandler<ExampleQuery, int>
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
                public class ExampleQuery : Minerals.AutoCQRS.Interfaces.IQuery
                {

                }

                public class ExampleQueryHandler : Minerals.AutoCQRS.Interfaces.IQueryHandler<ExampleQuery, int>
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
    }
}