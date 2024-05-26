namespace Minerals.AutoCQRS.Generators.Objects
{
    public readonly struct CommandPipelineObject : IEquatable<CommandPipelineObject>
    {
        public string Name { get; }
        public string Keyword { get; }
        public string Namespace { get; }
        public string[] AccessModifiers { get; }
        public string[] Modifiers { get; }
        public TypeArgumentObject[] TypeArguments { get; }

        public CommandPipelineObject(GeneratorSyntaxContext context)
        {
            Name = CodeBuilderHelper.GetIdentifierNameOf(context.Node);
            Keyword = GetKeywordOf(context.Node);
            Namespace = CodeBuilderHelper.GetNamespaceOf(context.Node) ?? string.Empty;
            AccessModifiers = CodeBuilderHelper.GetAccessModifiersOf(context.Node)?.ToArray() ?? [];
            Modifiers = CodeBuilderHelper.GetModifiersOf(context.Node).ToArray();
            TypeArguments = GetTypeArgumentsOf(context);
        }

        public bool Equals(CommandPipelineObject other)
        {
            return other.Name.Equals(Name)
                && other.Keyword.Equals(Keyword)
                && other.Namespace.Equals(Namespace)
                && other.AccessModifiers.SequenceEqual(AccessModifiers)
                && other.Modifiers.SequenceEqual(Modifiers)
                && other.TypeArguments.SequenceEqual(TypeArguments);
        }

        public override bool Equals(object? obj)
        {
            return obj is CommandPipelineObject other
                && other.Name.Equals(Name)
                && other.Keyword.Equals(Keyword)
                && other.Namespace.Equals(Namespace)
                && other.AccessModifiers.SequenceEqual(AccessModifiers)
                && other.Modifiers.SequenceEqual(Modifiers)
                && other.TypeArguments.SequenceEqual(TypeArguments);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Keyword, Namespace, AccessModifiers, Modifiers, TypeArguments);
        }

        private static string GetKeywordOf(SyntaxNode syntax)
        {
            return syntax is TypeDeclarationSyntax typeSyntax ? typeSyntax.Keyword.ToString() : string.Empty;
        }

        private static TypeArgumentObject[] GetTypeArgumentsOf(GeneratorSyntaxContext context)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(context.Node) as ITypeSymbol;
            var pipeline = symbol!.Interfaces.First(x => x.Name.Equals("ICommandPipeline"));
            return pipeline.TypeArguments.Select(x =>
            {
                return new TypeArgumentObject(x.Name, x.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
            }).ToArray();
        }
    }
}