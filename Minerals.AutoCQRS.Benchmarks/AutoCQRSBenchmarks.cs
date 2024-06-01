namespace Minerals.AutoCQRS.Benchmarks
{
    public class AutoCQRSBenchmarks
    {
        public BenchmarkGeneration Baseline { get; set; } = default!;
        public BenchmarkGeneration QueryGeneration { get; set; } = default!;
        public BenchmarkGeneration CommandGeneration { get; set; } = default!;
        public BenchmarkGeneration CommandAndQueryGeneration { get; set; } = default!;
        public BenchmarkGeneration BaselineDouble { get; set; } = default!;
        public BenchmarkGeneration QueryGenerationDouble { get; set; } = default!;
        public BenchmarkGeneration CommandGenerationDouble { get; set; } = default!;
        public BenchmarkGeneration CommandAndQueryGenerationDouble { get; set; } = default!;

        private const string _withoutInterfaces = """
        namespace Examples
        {
            using System.Threading.Tasks;
            using System.Threading;
            using Minerals.AutoCQRS;

            public class ExampleService1;
            public class ExampleService2;
            public class ExampleCommand;

            public class ExampleCommandHandler1
            {
                public ExampleCommandHandler1(ExampleService1 service1)
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler2
            {
                public ExampleCommandHandler2()
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler3
            {
                public ExampleCommandHandler3(ExampleService1 service1, ExampleService2 service2)
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public partial class ExampleCommandPipeline;

            public class ExampleQuery;

            public class ExampleQueryHandler1
            {
                public ExampleQueryHandler1(ExampleService1 service1)
                {

                }

                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleQueryHandler2
            {
                public ExampleQueryHandler2()
                {

                }

                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleQueryHandler3
            {
                public ExampleQueryHandler3(ExampleService1 service1, ExampleService2 service2)
                {

                }

                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public partial class ExampleQueryPipeline;
        }
        """;

        private const string _withQueryInterfaces = """
        namespace Examples
        {
            using System.Threading.Tasks;
            using System.Threading;
            using Minerals.AutoCQRS;

            public class ExampleService1;
            public class ExampleService2;
            public class ExampleCommand;

            public class ExampleCommandHandler1
            {
                public ExampleCommandHandler1(ExampleService1 service1)
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler2
            {
                public ExampleCommandHandler2()
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler3
            {
                public ExampleCommandHandler3(ExampleService1 service1, ExampleService2 service2)
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public partial class ExampleCommandPipeline;

            public class ExampleQuery : IQuery;

            public class ExampleQueryHandler1 : IQueryHandler<ExampleQuery, int>
            {
                public ExampleQueryHandler1(ExampleService1 service1)
                {

                }

                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleQueryHandler2 : IQueryHandler<ExampleQuery, int>
            {
                public ExampleQueryHandler2()
                {

                }

                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleQueryHandler3 : IQueryHandler<ExampleQuery, int>
            {
                public ExampleQueryHandler3(ExampleService1 service1, ExampleService2 service2)
                {

                }

                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public partial class ExampleQueryPipeline : IQueryPipeline<ExampleQuery, int, ExampleQueryHandler1, ExampleQueryHandler2, ExampleQueryHandler3>;
        }
        """;

        private const string _withCommandInterfaces = """
        namespace Examples
        {
            using System.Threading.Tasks;
            using System.Threading;
            using Minerals.AutoCQRS;

            public class ExampleService1;
            public class ExampleService2;
            public class ExampleCommand : ICommand;

            public class ExampleCommandHandler1 : ICommandHandler<ExampleCommand, int>
            {
                public ExampleCommandHandler1(ExampleService1 service1)
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler2 : ICommandHandler<ExampleCommand, int>
            {
                public ExampleCommandHandler2()
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler3 : ICommandHandler<ExampleCommand, int>
            {
                public ExampleCommandHandler3(ExampleService1 service1, ExampleService2 service2)
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public partial class ExampleCommandPipeline : ICommandPipeline<ExampleCommand, int, ExampleCommandHandler1, ExampleCommandHandler2, ExampleCommandHandler3>;

            public class ExampleQuery;

            public class ExampleQueryHandler1
            {
                public ExampleQueryHandler1(ExampleService1 service1)
                {

                }

                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleQueryHandler2
            {
                public ExampleQueryHandler2()
                {

                }

                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleQueryHandler3
            {
                public ExampleQueryHandler3(ExampleService1 service1, ExampleService2 service2)
                {

                }

                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public partial class ExampleQueryPipeline;
        }
        """;

        private const string _withCommandAndQueryInterfaces = """
        namespace Examples
        {
            using System.Threading.Tasks;
            using System.Threading;
            using Minerals.AutoCQRS;

            public class ExampleService1;
            public class ExampleService2;
            public class ExampleCommand : ICommand;

            public class ExampleCommandHandler1 : ICommandHandler<ExampleCommand, int>
            {
                public ExampleCommandHandler1(ExampleService1 service1)
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler2 : ICommandHandler<ExampleCommand, int>
            {
                public ExampleCommandHandler2()
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler3 : ICommandHandler<ExampleCommand, int>
            {
                public ExampleCommandHandler3(ExampleService1 service1, ExampleService2 service2)
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public partial class ExampleCommandPipeline : ICommandPipeline<ExampleCommand, int, ExampleCommandHandler1, ExampleCommandHandler2, ExampleCommandHandler3>;

            public class ExampleQuery : IQuery;

            public class ExampleQueryHandler1 : IQueryHandler<ExampleQuery, int>
            {
                public ExampleQueryHandler1(ExampleService1 service1)
                {

                }

                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleQueryHandler2 : IQueryHandler<ExampleQuery, int>
            {
                public ExampleQueryHandler2()
                {

                }

                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleQueryHandler3 : IQueryHandler<ExampleQuery, int>
            {
                public ExampleQueryHandler3(ExampleService1 service1, ExampleService2 service2)
                {

                }

                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation = default)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public partial class ExampleQueryPipeline : IQueryPipeline<ExampleQuery, int, ExampleQueryHandler1, ExampleQueryHandler2, ExampleQueryHandler3>;
        }
        """;

        [GlobalSetup]
        public void Initialize()
        {
            var references = BenchmarkGenerationExtensions.GetAppReferences
            (
                typeof(object),
                typeof(IQueryDispatcher),
                typeof(IServiceCollectionExtensionsGenerator),
                typeof(StringCases.StringExtensions),
                typeof(CodeBuilder)
            );
            Baseline = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withoutInterfaces,
                references
            );
            QueryGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withQueryInterfaces,
                new IServiceCollectionExtensionsGenerator(),
                references
            );
            CommandGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withCommandInterfaces,
                new IServiceCollectionExtensionsGenerator(),
                references
            );
            CommandAndQueryGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withCommandAndQueryInterfaces,
                new IServiceCollectionExtensionsGenerator(),
                references
            );
            BaselineDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withoutInterfaces,
                references
            );
            QueryGenerationDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withQueryInterfaces,
                new IServiceCollectionExtensionsGenerator(),
                references
            );
            CommandGenerationDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withCommandInterfaces,
                new IServiceCollectionExtensionsGenerator(),
                references
            );
            CommandAndQueryGenerationDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withCommandAndQueryInterfaces,
                new IServiceCollectionExtensionsGenerator(),
                references
            );
            BaselineDouble.RunAndSaveGeneration();
            BaselineDouble.AddSourceCode("// Test Comment");
            QueryGenerationDouble.RunAndSaveGeneration();
            QueryGenerationDouble.AddSourceCode("// Test Comment");
            CommandGenerationDouble.RunAndSaveGeneration();
            CommandGenerationDouble.AddSourceCode("// Test Comment");
            CommandAndQueryGenerationDouble.RunAndSaveGeneration();
            CommandAndQueryGenerationDouble.AddSourceCode("// Test Comment");
        }

        [Benchmark(Baseline = true)]
        public void SingleGeneration_Baseline()
        {
            Baseline.RunGeneration();
        }

        [Benchmark]
        public void SingleGeneration_Query()
        {
            QueryGeneration.RunGeneration();
        }

        [Benchmark]
        public void SingleGeneration_Command()
        {
            CommandGeneration.RunGeneration();
        }

        [Benchmark]
        public void SingleGeneration_CommandAndQuery()
        {
            CommandAndQueryGeneration.RunGeneration();
        }

        [Benchmark]
        public void DoubleGeneration_Baseline()
        {
            BaselineDouble.RunGeneration();
        }

        [Benchmark]
        public void DoubleGeneration_Query()
        {
            QueryGenerationDouble.RunGeneration();
        }

        [Benchmark]
        public void DoubleGeneration_Command()
        {
            CommandGenerationDouble.RunGeneration();
        }

        [Benchmark]
        public void DoubleGeneration_CommandAndQuery()
        {
            CommandAndQueryGenerationDouble.RunGeneration();
        }
    }
}