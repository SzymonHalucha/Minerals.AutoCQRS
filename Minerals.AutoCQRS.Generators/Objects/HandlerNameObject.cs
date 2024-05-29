namespace Minerals.AutoCQRS.Generators.Objects
{
    public record HandlerNameObject : NameObject
    {
        public HandlerNameObject(ITypeSymbol symbol) : base
        (
            GetTypeNameOf(symbol),
            GetFullTypeNameOf(symbol),
            GetInterfaceFullTypeNameOf(symbol)
        )
        { }

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
                return (x.Name.Equals(Constants.ICommandHandler) || x.Name.Equals(Constants.IQueryHandler))
                    && x.ContainingNamespace.Name.Equals(nameof(AutoCQRS))
                    && x.ContainingNamespace.ContainingNamespace.Name.Equals(nameof(Minerals));
            }).ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }
    }
}