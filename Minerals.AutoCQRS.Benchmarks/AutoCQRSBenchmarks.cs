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

        private const string s_withoutInterfaces = """
        namespace Minerals.Examples
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

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler2
            {
                public ExampleCommandHandler2()
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler3
            {
                public ExampleCommandHandler3(ExampleService1 service1, ExampleService2 service2)
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public partial class ExampleCommandPipeline;

            public class ExampleQuery;

            public class ExampleQueryHandler1
            {
                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleQueryHandler2
            {
                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleQueryHandler3
            {
                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }
        }
        """;

        private const string s_withQueryInterfaces = """
        namespace Minerals.Examples
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

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler2
            {
                public ExampleCommandHandler2()
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler3
            {
                public ExampleCommandHandler3(ExampleService1 service1, ExampleService2 service2)
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public partial class ExampleCommandPipeline;

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

            public class ExampleQueryHandler3 : IQueryHandler<ExampleQuery, int>
            {
                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }
        }
        """;

        private const string s_withCommandInterfaces = """
        namespace Minerals.Examples
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

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler2 : ICommandHandler<ExampleCommand, int>
            {
                public ExampleCommandHandler2()
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler3 : ICommandHandler<ExampleCommand, int>
            {
                public ExampleCommandHandler3(ExampleService1 service1, ExampleService2 service2)
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public partial class ExampleCommandPipeline : ICommandPipeline<ExampleCommand, int, ExampleCommandHandler1, ExampleCommandHandler2, ExampleCommandHandler3>;

            public class ExampleQuery;

            public class ExampleQueryHandler1
            {
                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleQueryHandler2
            {
                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleQueryHandler3
            {
                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }
        }
        """;

        private const string s_withCommandAndQueryInterfaces = """
        namespace Minerals.Examples
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

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler2 : ICommandHandler<ExampleCommand, int>
            {
                public ExampleCommandHandler2()
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleCommandHandler3 : ICommandHandler<ExampleCommand, int>
            {
                public ExampleCommandHandler3(ExampleService1 service1, ExampleService2 service2)
                {

                }

                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public partial class ExampleCommandPipeline : ICommandPipeline<ExampleCommand, int, ExampleCommandHandler1, ExampleCommandHandler2, ExampleCommandHandler3>;

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

            public class ExampleQueryHandler3 : IQueryHandler<ExampleQuery, int>
            {
                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }
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

                typeof(CodeBuilder)
            );
            Baseline = BenchmarkGenerationExtensions.CreateGeneration
            (
                s_withoutInterfaces,
                references
            );
            QueryGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                s_withQueryInterfaces,
                new IServiceCollectionExtensionsGenerator(),
                references
            );
            CommandGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                s_withCommandInterfaces,
                new IServiceCollectionExtensionsGenerator(),
                references
            );
            CommandAndQueryGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                s_withCommandAndQueryInterfaces,
                new IServiceCollectionExtensionsGenerator(),
                references
            );
            BaselineDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                s_withoutInterfaces,
                references
            );
            QueryGenerationDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                s_withQueryInterfaces,
                new IServiceCollectionExtensionsGenerator(),
                references
            );
            CommandGenerationDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                s_withCommandInterfaces,
                new IServiceCollectionExtensionsGenerator(),
                references
            );
            CommandAndQueryGenerationDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                s_withCommandAndQueryInterfaces,
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