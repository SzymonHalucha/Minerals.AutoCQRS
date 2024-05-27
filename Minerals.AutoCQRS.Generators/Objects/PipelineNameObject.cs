namespace Minerals.AutoCQRS.Generators.Objects
{
    public record PipelineNameObject : NameObject
    {
        public NameObject[] TypeArguments { get; }

        public PipelineNameObject(ITypeSymbol symbol) : base
        (
            GetTypeNameOf(symbol),
            GetFullTypeNameOf(symbol),
            GetInterfaceFullTypeNameOf(symbol)
        )
        {
            TypeArguments = GetTypeArgumentsOf(symbol);
        }

        public virtual bool Equals(PipelineNameObject other)
        {
            return other.FullTypeName.Equals(FullTypeName)
                && other.InterfaceFullTypeName.Equals(InterfaceFullTypeName)
                && other.TypeArguments.SequenceEqual(TypeArguments);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullTypeName, InterfaceFullTypeName, TypeArguments);
        }

        private static string GetTypeNameOf(ITypeSymbol symbol)
        {
            return symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
        }

        private static string GetFullTypeNameOf(ITypeSymbol symbol)
        {
            return symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }

        private static string GetInterfaceFullTypeNameOf(ITypeSymbol symbol)
        {
            return symbol.Interfaces.First(x =>
            {
                return x.Name.Equals("ICommandPipeline")
                    && x.ContainingNamespace.Name.Equals(nameof(AutoCQRS))
                    && x.ContainingNamespace.ContainingNamespace.Name.Equals(nameof(Minerals));
            }).Interfaces[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }

        private static NameObject[] GetTypeArgumentsOf(ITypeSymbol typeSymbol)
        {
            return typeSymbol!.Interfaces.First(symbol =>
            {
                return symbol.Name.Equals("ICommandPipeline")
                    && symbol.ContainingNamespace.Name.Equals(nameof(AutoCQRS))
                    && symbol.ContainingNamespace.ContainingNamespace.Name.Equals(nameof(Minerals));
            }).TypeArguments
            .Select(selected =>
            {
                return new NameObject
                (
                    selected.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
                    selected.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                    selected.Interfaces[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
                );
            }).ToArray();
        }
    }
}