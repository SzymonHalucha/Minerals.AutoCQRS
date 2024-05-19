using Minerals.AutoCQRS.Interfaces;

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
        namespaces Minerals.Examples
        {
            public class ExampleCommand
            {

            }

            public class ExampleCommandHandler
            {
                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

            public class ExampleQuery
            {

            }

            public class ExampleQueryHandler
            {
                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }
        }
        """;

        private const string _withQueryInterfaces = """
        namespaces Minerals.Examples
        {
            public class ExampleCommand
            {

            }

            public class ExampleCommandHandler
            {
                public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }

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

        private const string _withCommandInterfaces = """
        namespaces Minerals.Examples
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

            public class ExampleQuery
            {

            }

            public class ExampleQueryHandler
            {
                public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
                {
                    return Task.FromResult<int>(default!);
                }
            }
        }
        """;

        private const string _withCommandAndQueryInterfaces = """
        namespaces Minerals.Examples
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


        [GlobalSetup]
        public void Initialize()
        {
            var references = BenchmarkGenerationExtensions.GetAppReferences
            (
                typeof(object),
                typeof(IQueryDispatcher),
                typeof(ServiceCollectionExtensionsGenerator),

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
                new ServiceCollectionExtensionsGenerator(),
                references
            );
            CommandGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withCommandInterfaces,
                new ServiceCollectionExtensionsGenerator(),
                references
            );
            CommandAndQueryGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withCommandAndQueryInterfaces,
                new ServiceCollectionExtensionsGenerator(),
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
                new ServiceCollectionExtensionsGenerator(),
                references
            );
            CommandGenerationDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withCommandInterfaces,
                new ServiceCollectionExtensionsGenerator(),
                references
            );
            CommandAndQueryGenerationDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                _withCommandAndQueryInterfaces,
                new ServiceCollectionExtensionsGenerator(),
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