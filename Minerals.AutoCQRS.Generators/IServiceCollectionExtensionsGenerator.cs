namespace Minerals.AutoCQRS.Generators
{
    [Generator]
    public sealed class IServiceCollectionExtensionsGenerator : IIncrementalGenerator
    {
        private static readonly string[] s_handlersDispatchers =
        [
            "global::Minerals.AutoCQRS.ICommandDispatcher, global::Minerals.AutoCQRS.CommandDispatcher",
            "global::Minerals.AutoCQRS.IQueryDispatcher, global::Minerals.AutoCQRS.QueryDispatcher"
        ];

        private static readonly string[] s_pipelinesDispatchers =
        [
            "global::Minerals.AutoCQRS.ICommandPipelineDispatcher, global::Minerals.AutoCQRS.CommandPipelineDispatcher",
            // "global::Minerals.AutoCQRS.IQueryPipelineDispatcher, global::Minerals.AutoCQRS.QueryPipelineDispatcher"
        ];

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var founded = context.SyntaxProvider.CreateSyntaxProvider
            (
                static (x, _) => CheckForInterfaces(x),
                static (x, _) => x
            );

            var validHandlers = founded.Where(HasValidInterfaceForHandler);
            validHandlers = validHandlers.Where(HasNoAttribute);
            var selectedHandlers = validHandlers.Select(GetHandlerObjectForCommandsAndQueries);
            var collectedHandlers = selectedHandlers.Collect();

            var validPipelines = founded.Where(HasValidInterfaceForPipeline);
            var selectedPipelines = validPipelines.Select(GetHandlerObjectForPipelines);
            var collectedPipelines = selectedPipelines.Collect();

            var combined = collectedHandlers.Combine(collectedPipelines);

            context.RegisterSourceOutput(combined, static (ctx, collected) =>
            {
                ctx.AddSource
                (
                    "IServiceCollectionExtensions.g.cs",
                    GenerateStaticClass(collected.Left, collected.Right)
                );
            });
        }

        private static bool CheckForInterfaces(SyntaxNode node)
        {
            return node is (not InterfaceDeclarationSyntax) and TypeDeclarationSyntax typeSyntax
                && typeSyntax.BaseList is not null
                && typeSyntax.BaseList.Types.Count > 0;
        }

        private static bool HasValidInterfaceForHandler(GeneratorSyntaxContext context)
        {
            return ((ITypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node)!)
                .Interfaces.Any(x =>
                {
                    return (x.Name.Equals("ICommandHandler") || x.Name.Equals("IQueryHandler"))
                        && x.ContainingNamespace.Name.Equals(nameof(AutoCQRS))
                        && x.ContainingNamespace.ContainingNamespace.Name.Equals(nameof(Minerals));
                });
        }

        //TODO: Implement this...
        private static bool HasNoAttribute(GeneratorSyntaxContext context)
        {
            return false;
        }

        private static bool HasValidInterfaceForPipeline(GeneratorSyntaxContext context)
        {
            return ((ITypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node)!)
                .Interfaces.Any(x =>
                {
                    return x.Name.Equals("ICommandPipeline")
                        && x.ContainingNamespace.Name.Equals(nameof(AutoCQRS))
                        && x.ContainingNamespace.ContainingNamespace.Name.Equals(nameof(Minerals));
                });
        }

        private static HandlerObject GetHandlerObjectForCommandsAndQueries(GeneratorSyntaxContext context, CancellationToken cancellation)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(context.Node) as ITypeSymbol;
            var handler = new HandlerObject
            (
                symbol!.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                symbol!.Interfaces.First(x =>
                {
                    return (x.Name.Equals("ICommandHandler") || x.Name.Equals("IQueryHandler"))
                        && x.ContainingNamespace.Name.Equals(nameof(AutoCQRS))
                        && x.ContainingNamespace.ContainingNamespace.Name.Equals(nameof(Minerals));
                }).ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
            );
            return handler;
        }

        private static HandlerObject GetHandlerObjectForPipelines(GeneratorSyntaxContext context, CancellationToken cancellation)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(context.Node) as ITypeSymbol;
            var handler = new HandlerObject
            (
                symbol!.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                symbol!.Interfaces.First(x =>
                {
                    return x.Name.Equals("ICommandPipeline")
                        && x.ContainingNamespace.Name.Equals(nameof(AutoCQRS))
                        && x.ContainingNamespace.ContainingNamespace.Name.Equals(nameof(Minerals));
                }).Interfaces[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
            );
            return handler;
        }

        private static SourceText GenerateStaticClass(ImmutableArray<HandlerObject> handlers, ImmutableArray<HandlerObject> pipelines)
        {
            var builder = new CodeBuilder();
            builder.AddAutoGeneratedHeader(Assembly.GetExecutingAssembly());
            AppendNamespace(builder);

            builder.AddAutoGeneratedAttributes(typeof(ClassDeclarationSyntax));
            AppendStaticClassHeader(builder);
            AppendExtensionMethod(builder, handlers, s_handlersDispatchers, "AddCommandsAndQueries");
            AppendExtensionMethod(builder, pipelines, s_pipelinesDispatchers, "AddCommandsAndQueriesPipelines");
            AppendDefaultInjectPolicyMethod(builder);

            builder.CloseAllBlocks();
            return SourceText.From(builder.ToString(), Encoding.UTF8);
        }

        private static void AppendNamespace(CodeBuilder builder)
        {
            builder.WriteLine("namespace Minerals.AutoCQRS")
                .OpenBlock()
                .WriteLine("using global::Microsoft.Extensions.DependencyInjection.Extensions;")
                .WriteLine("using global::Microsoft.Extensions.DependencyInjection;");
        }

        private static void AppendStaticClassHeader(CodeBuilder builder)
        {
            builder.WriteLine("public static class IServiceCollectionExtensions")
                .OpenBlock();
        }

        private static void AppendExtensionMethod(CodeBuilder builder, ImmutableArray<HandlerObject> items, IEnumerable<string> dispatchers, string methodName)
        {
            builder.WriteLine("public static global::Microsoft.Extensions.DependencyInjection.IServiceCollection ")
                .Write(methodName)
                .Write("(this global::Microsoft.Extensions.DependencyInjection.IServiceCollection collection, ")
                .Write("global::System.Action<global::Microsoft.Extensions.DependencyInjection.IServiceCollection, global::System.Type, global::System.Type> injectPolicy = null)")
                .OpenBlock();

            builder.WriteLine("if (injectPolicy is null)")
                .OpenBlock()
                .WriteLine("injectPolicy = DefaultInjectPolicy;")
                .CloseBlock();

            foreach (var item in dispatchers)
            {
                builder.WriteLine("collection.TryAddSingleton<")
                    .Write(item)
                    .Write(">();");
            }

            foreach (var item in items)
            {
                builder.WriteLine("injectPolicy.Invoke(collection, typeof(")
                    .Write(item.InterfaceFullTypeName)
                    .Write("), typeof(")
                    .Write(item.FullTypeName)
                    .Write("));");
            }

            builder.WriteLine("return collection;")
                .CloseBlock()
                .NewLine();
        }

        private static void AppendDefaultInjectPolicyMethod(CodeBuilder builder)
        {
            builder.WriteLine("private static void DefaultInjectPolicy(global::Microsoft.Extensions.DependencyInjection.IServiceCollection collection, global::System.Type interfaceType, global::System.Type serviceType)")
                .OpenBlock()
                .WriteLine("collection.AddSingleton(interfaceType, serviceType);")
                .CloseBlock();
        }
    }
}