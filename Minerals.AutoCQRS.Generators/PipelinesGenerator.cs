namespace Minerals.AutoCQRS.Generators
{
    [Generator]
    public sealed class PipelinesGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var pipelines = context.SyntaxProvider.CreateSyntaxProvider
            (
                static (x, _) => CheckForInterfaces(x),
                static (x, _) => GetValidOrNullPipelineObject(x)
            ).Where(x => x is not null);

            var notNullPipelines = pipelines.Select((x, _) => (PipelineObject)x!);

            context.RegisterSourceOutput(notNullPipelines, static (ctx, element) =>
            {
                ctx.AddSource($"{element.Name}.g.cs", GeneratePartialPipelineObject(element));
            });
        }

        private static bool CheckForInterfaces(SyntaxNode node)
        {
            return node is (not InterfaceDeclarationSyntax) and TypeDeclarationSyntax typeSyntax
                && typeSyntax.BaseList is not null
                && typeSyntax.BaseList.Types.Count > 0;
        }

        private static PipelineObject? GetValidOrNullPipelineObject(GeneratorSyntaxContext context)
        {
            var symbol = (ITypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node)!;
            return HasValidInterface(symbol) ? new PipelineObject(context, symbol) : null;
        }

        private static bool HasValidInterface(ITypeSymbol symbol)
        {
            return symbol.Interfaces.Any(x =>
            {
                return (x.Name.Equals(Constants.ICommandPipeline) || x.Name.Equals(Constants.IQueryPipeline))
                    && x.ContainingNamespace.Name.Equals(nameof(AutoCQRS))
                    && x.ContainingNamespace.ContainingNamespace.Name.Equals(nameof(Minerals));
            });
        }

        private static SourceText GeneratePartialPipelineObject(PipelineObject pipelineObj)
        {
            var builder = new CodeBuilder();
            builder.AddAutoGeneratedHeader(Assembly.GetExecutingAssembly());
            AppendNamespace(builder, pipelineObj);

            builder.AddAutoGeneratedAttributes(typeof(ClassDeclarationSyntax));
            AppendPartialObjectHeader(builder, pipelineObj);
            builder.OpenBlock();

            AppendProperties(builder, pipelineObj);
            AppendConstructorHeader(builder, pipelineObj);
            AppendConstructorBody(builder, pipelineObj);
            AppendHandleMethod(builder, pipelineObj);

            builder.CloseAllBlocks();
            return SourceText.From(builder.ToString(), Encoding.UTF8);
        }

        private static void AppendNamespace(CodeBuilder builder, PipelineObject pipelineObj)
        {
            if (pipelineObj.Namespace != string.Empty)
            {
                builder.WriteLine("namespace ").Write(pipelineObj.Namespace).OpenBlock();
            }
        }

        private static void AppendPartialObjectHeader(CodeBuilder builder, PipelineObject pipelineObj)
        {
            builder.NewLine().WriteIteration(pipelineObj.Modifiers, (builder1, item, next) =>
            {
                builder1.Write(item).Write(" ");
            });
            builder.Write(pipelineObj.Keyword).Write(" ").Write(pipelineObj.Name);
        }

        private static void AppendProperties(CodeBuilder builder, PipelineObject pipelineObj)
        {
            foreach (var arg in pipelineObj.TypeArguments.Skip(2))
            {
                builder.WriteLine("private readonly ")
                    .Write(arg.FullTypeName)
                    .Write(" _")
                    .Write(arg.CamelCaseTypeName)
                    .Write(";");
            }
            builder.NewLine();
        }

        private static void AppendConstructorHeader(CodeBuilder builder, PipelineObject pipelineObj)
        {
            builder.NewLine().WriteIteration(pipelineObj.AccessModifiers, (builder1, item, next) =>
            {
                builder1.Write(item)
                    .Write(" ");
            });

            builder.Write(pipelineObj.Name)
                .Write("(");

            var arguments = pipelineObj.TypeArguments
                .Skip(2)
                .SelectMany(x => x.ConstructorArguments)
                .Distinct();

            builder.WriteIteration(arguments, (builder1, item, next) =>
            {
                builder1.Write(item.FullTypeName)
                    .Write(" ")
                    .Write(item.CamelCaseTypeName);
                if (next)
                {
                    builder1.Write(", ");
                }
            });
            builder.Write(")").OpenBlock();
        }

        private static void AppendConstructorBody(CodeBuilder builder, PipelineObject pipelineObj)
        {
            foreach (var arg in pipelineObj.TypeArguments.Skip(2))
            {
                builder.WriteLine("_")
                    .Write(arg.CamelCaseTypeName)
                    .Write(" = new ")
                    .Write(arg.FullTypeName)
                    .Write("(");

                builder.WriteIteration(arg.ConstructorArguments, (builder1, item, next) =>
                {
                    builder1.Write(item.CamelCaseTypeName);
                    if (next)
                    {
                        builder1.Write(", ");
                    }
                });
                builder.Write(");");
            }
            builder.CloseBlock()
                .NewLine();
        }

        private static void AppendHandleMethod(CodeBuilder builder, PipelineObject pipelineObj)
        {
            builder.WriteLine("public async global::System.Collections.Generic.IAsyncEnumerable<")
                .Write(pipelineObj.TypeArguments[1].FullTypeName)
                .Write("> Handle(")
                .Write(pipelineObj.TypeArguments[0].FullTypeName)
                .Write(" item, [global::System.Runtime.CompilerServices.EnumeratorCancellationAttribute]")
                .Write(" global::System.Threading.CancellationToken cancellation)")
                .OpenBlock();

            foreach (var arg in pipelineObj.TypeArguments.Skip(2))
            {
                builder.WriteLine("yield return await _")
                    .Write(arg.CamelCaseTypeName)
                    .Write(".Handle(item, cancellation);");
            }

            builder.CloseBlock();
        }
    }
}