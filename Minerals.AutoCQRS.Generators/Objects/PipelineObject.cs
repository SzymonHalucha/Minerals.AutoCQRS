namespace Minerals.AutoCQRS.Generators.Objects
{
    public readonly struct PipelineObject : IEquatable<PipelineObject>
    {
        public string Name { get; }
        public string Keyword { get; }
        public string Namespace { get; }
        public string[] AccessModifiers { get; }
        public string[] Modifiers { get; }
        public TypeArgumentObject[] TypeArguments { get; }
        public ITypeSymbol Symbol { get; }

        public PipelineObject(GeneratorSyntaxContext context, ITypeSymbol symbol)
        {
            Name = CodeBuilderHelper.GetIdentifierNameOf(context.Node);
            Keyword = GetKeywordOf(context.Node);
            Namespace = CodeBuilderHelper.GetNamespaceOf(context.Node) ?? string.Empty;
            AccessModifiers = CodeBuilderHelper.GetAccessModifiersOf(context.Node)?.ToArray() ?? [];
            Modifiers = CodeBuilderHelper.GetModifiersOf(context.Node).ToArray();
            Symbol = symbol;
            TypeArguments = GetTypeArgumentsOf(Symbol);
        }

        public bool Equals(PipelineObject other)
        {
            return other.Name.Equals(Name)
                && other.Keyword.Equals(Keyword)
                && other.Namespace.Equals(Namespace)
                && other.AccessModifiers.SequenceEqual(AccessModifiers)
                && other.Modifiers.SequenceEqual(Modifiers)
                && SymbolEqualityComparer.Default.Equals(other.Symbol, Symbol)
                && other.TypeArguments.SequenceEqual(TypeArguments);
        }

        public override bool Equals(object? obj)
        {
            return obj is PipelineObject other
                && other.Name.Equals(Name)
                && other.Keyword.Equals(Keyword)
                && other.Namespace.Equals(Namespace)
                && other.AccessModifiers.SequenceEqual(AccessModifiers)
                && other.Modifiers.SequenceEqual(Modifiers)
                && SymbolEqualityComparer.Default.Equals(other.Symbol, Symbol)
                && other.TypeArguments.SequenceEqual(TypeArguments);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine
            (
                Name,
                Keyword,
                Namespace,
                AccessModifiers,
                Modifiers,
                SymbolEqualityComparer.Default.GetHashCode(Symbol),
                TypeArguments
            );
        }

        private static string GetKeywordOf(SyntaxNode syntax)
        {
            return syntax is TypeDeclarationSyntax typeSyntax ? typeSyntax.Keyword.ToString() : string.Empty;
        }

        private static ITypeSymbol GetTypeSymbolOf(GeneratorSyntaxContext context)
        {
            return (ITypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node)!;
        }

        private static TypeArgumentObject[] GetTypeArgumentsOf(ITypeSymbol typeSymbol)
        {
            return typeSymbol!.Interfaces.First(symbol =>
            {
                return (symbol.Name.Equals(Constants.ICommandPipeline) || symbol.Name.Equals(Constants.IQueryPipeline))
                    && symbol.ContainingNamespace.Name.Equals(nameof(AutoCQRS))
                    && symbol.ContainingNamespace.ContainingNamespace.Name.Equals(nameof(Minerals));
            }).TypeArguments.Select(selected => new TypeArgumentObject(selected)).ToArray();
        }
    }
}