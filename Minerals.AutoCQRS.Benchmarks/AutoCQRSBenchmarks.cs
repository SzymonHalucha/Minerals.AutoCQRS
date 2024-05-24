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
            public class ExampleCommand
            {

            }

            public class ExampleCommandHandler
            {
                public System.Threading.Tasks.Task<int> Handle(ExampleCommand command, System.Threading.CancellationToken cancellation)
                {
                    return System.Threading.Tasks.Task.FromResult<int>(default!);
                }
            }

            public class ExampleQuery
            {

            }

            public class ExampleQueryHandler
            {
                public System.Threading.Tasks.Task<int> Handle(ExampleQuery query, System.Threading.CancellationToken cancellation)
                {
                    return System.Threading.Tasks.Task.FromResult<int>(default!);
                }
            }
        }
        """;

        private const string s_withQueryInterfaces = """
        namespace Minerals.Examples
        {
            public class ExampleCommand
            {

            }

            public class ExampleCommandHandler
            {
                public System.Threading.Tasks.Task<int> Handle(ExampleCommand command, System.Threading.CancellationToken cancellation)
                {
                    return System.Threading.Tasks.Task.FromResult<int>(default!);
                }
            }

            public class ExampleQuery : Minerals.AutoCQRS.Interfaces.IQuery
            {

            }

            public class ExampleQueryHandler : Minerals.AutoCQRS.Interfaces.IQueryHandler<ExampleQuery, int>
            {
                public System.Threading.Tasks.Task<int> Handle(ExampleQuery query, System.Threading.CancellationToken cancellation)
                {
                    return System.Threading.Tasks.Task.FromResult<int>(default!);
                }
            }
        }
        """;

        private const string s_withCommandInterfaces = """
        namespace Minerals.Examples
        {
            public class ExampleCommand : Minerals.AutoCQRS.Interfaces.ICommand
            {

            }

            public class ExampleCommandHandler : Minerals.AutoCQRS.Interfaces.ICommandHandler<ExampleCommand, int>
            {
                public System.Threading.Tasks.Task<int> Handle(ExampleCommand command, System.Threading.CancellationToken cancellation)
                {
                    return System.Threading.Tasks.Task.FromResult<int>(default!);
                }
            }

            public class ExampleQuery
            {

            }

            public class ExampleQueryHandler
            {
                public System.Threading.Tasks.Task<int> Handle(ExampleQuery query, System.Threading.CancellationToken cancellation)
                {
                    return System.Threading.Tasks.Task.FromResult<int>(default!);
                }
            }
        }
        """;

        private const string s_withCommandAndQueryInterfaces = """
        namespace Minerals.Examples
        {
            public class ExampleCommand : Minerals.AutoCQRS.Interfaces.ICommand
            {

            }

            public class ExampleCommandHandler : Minerals.AutoCQRS.Interfaces.ICommandHandler<ExampleCommand, int>
            {
                public System.Threading.Tasks.Task<int> Handle(ExampleCommand command, System.Threading.CancellationToken cancellation)
                {
                    return System.Threading.Tasks.Task.FromResult<int>(default!);
                }
            }

            public class ExampleQuery : Minerals.AutoCQRS.Interfaces.IQuery
            {

            }

            public class ExampleQueryHandler : Minerals.AutoCQRS.Interfaces.IQueryHandler<ExampleQuery, int>
            {
                public System.Threading.Tasks.Task<int> Handle(ExampleQuery query, System.Threading.CancellationToken cancellation)
                {
                    return System.Threading.Tasks.Task.FromResult<int>(default!);
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
                s_withoutInterfaces,
                references
            );
            QueryGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                s_withQueryInterfaces,
                new ServiceCollectionExtensionsGenerator(),
                references
            );
            CommandGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                s_withCommandInterfaces,
                new ServiceCollectionExtensionsGenerator(),
                references
            );
            CommandAndQueryGeneration = BenchmarkGenerationExtensions.CreateGeneration
            (
                s_withCommandAndQueryInterfaces,
                new ServiceCollectionExtensionsGenerator(),
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
                new ServiceCollectionExtensionsGenerator(),
                references
            );
            CommandGenerationDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                s_withCommandInterfaces,
                new ServiceCollectionExtensionsGenerator(),
                references
            );
            CommandAndQueryGenerationDouble = BenchmarkGenerationExtensions.CreateGeneration
            (
                s_withCommandAndQueryInterfaces,
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