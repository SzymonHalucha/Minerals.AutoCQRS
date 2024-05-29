namespace Minerals.AutoCQRS.Generators.Objects
{
    public record TypeArgumentObject : NameObject
    {
        public NameObject[] ConstructorArguments { get; }

        public TypeArgumentObject(ITypeSymbol symbol) : base
        (
            GetTypeNameOf(symbol),
            GetFullTypeNameOf(symbol),
            GetInterfaceFullTypeNameOf(symbol)
        )
        {
            ConstructorArguments = GetConstructorArgumentsOf((INamedTypeSymbol)symbol);
        }

        public virtual bool Equals(TypeArgumentObject other)
        {
            return other.FullTypeName.Equals(FullTypeName)
                && other.InterfaceFullTypeName.Equals(InterfaceFullTypeName)
                && other.ConstructorArguments.SequenceEqual(ConstructorArguments);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullTypeName, InterfaceFullTypeName, ConstructorArguments);
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
            return symbol.Interfaces.FirstOrDefault(x =>
            {
                return (x.Name.Equals(Constants.ICommandHandler) || x.Name.Equals(Constants.IQueryHandler))
                    && x.ContainingNamespace.Name.Equals(nameof(AutoCQRS))
                    && x.ContainingNamespace.ContainingNamespace.Name.Equals(nameof(Minerals));
            })?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? string.Empty;
        }

        private static NameObject[] GetConstructorArgumentsOf(INamedTypeSymbol symbol)
        {
            return symbol.Constructors.SelectMany(x => x.Parameters)
                .Where(x => x is not null)
                .Select(x =>
                {
                    return new NameObject
                    (
                        x!.Type.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
                        x.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                        string.Empty
                    );
                }).ToArray();
        }
    }
}