using Minerals.StringCases;

namespace Minerals.AutoCQRS.Generators.Objects
{
    public readonly struct TypeArgumentObject(string typeName, string fullTypeName) : IEquatable<TypeArgumentObject>
    {
        public string CamelCaseTypeName { get; } = typeName.ToCamelCase();
        public string FullTypeName { get; } = fullTypeName;

        public bool Equals(TypeArgumentObject other)
        {
            return other.CamelCaseTypeName.Equals(CamelCaseTypeName)
                && other.FullTypeName.Equals(FullTypeName);
        }

        public override bool Equals(object? obj)
        {
            return obj is TypeArgumentObject other
                && other.CamelCaseTypeName.Equals(CamelCaseTypeName)
                && other.FullTypeName.Equals(FullTypeName);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CamelCaseTypeName, FullTypeName);
        }
    }
}