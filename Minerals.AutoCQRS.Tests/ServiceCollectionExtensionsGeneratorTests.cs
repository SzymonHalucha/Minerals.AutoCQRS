namespace Minerals.AutoCQRS.Tests
{
    [TestClass]
    public class ServiceCollectionExtensionsGeneratorTests : VerifyBase
    {
        public ServiceCollectionExtensionsGeneratorTests()
        {
            var references = VerifyExtensions.GetAppReferences
            (
                typeof(object),
                typeof(CodeBuilder),
                typeof(Interfaces.ICommandHandler<,>),
                typeof(ServiceCollectionExtensionsGenerator),
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
            return this.VerifyIncrementalGenerators(source, new ServiceCollectionExtensionsGenerator());
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
            return this.VerifyIncrementalGenerators(source, new ServiceCollectionExtensionsGenerator());
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
            return this.VerifyIncrementalGenerators(source, new ServiceCollectionExtensionsGenerator());
        }
    }
}