namespace Minerals.AutoCQRS.Generators
{
    [Generator]
    public sealed class CommandPipelineGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var pipelines = context.SyntaxProvider.CreateSyntaxProvider
            (
                static (x, _) => CheckForInterfaces(x),
                static (x, _) => x
            );

            pipelines = pipelines.Where(HasValidInterface);
            var selected = pipelines.Select(GetHandlerObject);

            context.RegisterSourceOutput(selected, static (ctx, element) =>
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

        private static bool HasValidInterface(GeneratorSyntaxContext context)
        {
            return ((ITypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node)!)
                .Interfaces.Any(x =>
                {
                    return x.Name.Equals("ICommandPipeline")
                        && x.ContainingNamespace.Name.Equals(nameof(AutoCQRS))
                        && x.ContainingNamespace.ContainingNamespace.Name.Equals(nameof(Minerals));
                });
        }

        private static CommandPipelineObject GetHandlerObject(GeneratorSyntaxContext context, CancellationToken cancellation)
        {
            return new CommandPipelineObject(context);
        }

        private static SourceText GeneratePartialPipelineObject(CommandPipelineObject pipelineObj)
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

        private static void AppendNamespace(CodeBuilder builder, CommandPipelineObject pipelineObj)
        {
            if (pipelineObj.Namespace != string.Empty)
            {
                builder.WriteLine("namespace ").Write(pipelineObj.Namespace).OpenBlock();
            }
        }

        private static void AppendPartialObjectHeader(CodeBuilder builder, CommandPipelineObject pipelineObj)
        {
            builder.NewLine().WriteIteration(pipelineObj.Modifiers, (builder1, item, next) =>
            {
                builder1.Write(item).Write(" ");
            });
            builder.Write(pipelineObj.Keyword).Write(" ").Write(pipelineObj.Name);
        }

        private static void AppendProperties(CodeBuilder builder, CommandPipelineObject pipelineObj)
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

        private static void AppendConstructorHeader(CodeBuilder builder, CommandPipelineObject pipelineObj)
        {
            builder.NewLine();
            foreach (var modifier in pipelineObj.AccessModifiers)
            {
                builder.Write(modifier).Write(" ");
            }

            builder.Write(pipelineObj.Name)
                .Write("(")
                .Write(pipelineObj.TypeArguments[2].FullTypeName)
                .Write(" ")
                .Write(pipelineObj.TypeArguments[2].CamelCaseTypeName);

            foreach (var arg in pipelineObj.TypeArguments.Skip(3))
            {
                builder.Write(", ")
                    .Write(arg.FullTypeName)
                    .Write(" ")
                    .Write(arg.CamelCaseTypeName);
            }
            builder.Write(")")
                .OpenBlock();
        }

        private static void AppendConstructorBody(CodeBuilder builder, CommandPipelineObject pipelineObj)
        {
            foreach (var arg in pipelineObj.TypeArguments.Skip(2))
            {
                builder.WriteLine("_")
                    .Write(arg.CamelCaseTypeName)
                    .Write(" = ")
                    .Write(arg.CamelCaseTypeName)
                    .Write(";");
            }
            builder.CloseBlock()
                .NewLine();
        }

        private static void AppendHandleMethod(CodeBuilder builder, CommandPipelineObject pipelineObj)
        {
            builder.WriteLine("public async global::System.Collections.Generic.IAsyncEnumerable<")
                .Write(pipelineObj.TypeArguments[1].FullTypeName)
                .Write("> Handle(")
                .Write(pipelineObj.TypeArguments[0].FullTypeName)
                .Write(" command, [global::System.Runtime.CompilerServices.EnumeratorCancellationAttribute]")
                .Write(" global::System.Threading.CancellationToken cancellation)")
                .OpenBlock();

            foreach (var arg in pipelineObj.TypeArguments.Skip(2))
            {
                builder.WriteLine("yield return await _")
                    .Write(arg.CamelCaseTypeName)
                    .Write(".Handle(command, cancellation);");
            }

            builder.CloseBlock();
        }
    }
}