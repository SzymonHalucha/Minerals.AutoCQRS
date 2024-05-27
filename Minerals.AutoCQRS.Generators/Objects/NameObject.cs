using Minerals.StringCases;

namespace Minerals.AutoCQRS.Generators.Objects
{
    public record NameObject : IEquatable<NameObject>
    {
        public string TypeName { get; }
        public string FullTypeName { get; }
        public string InterfaceFullTypeName { get; }
        public string CamelCaseTypeName { get; }

        public NameObject(string typeName, string fullTypeName, string interfaceFullTypeName)
        {
            TypeName = typeName;
            FullTypeName = fullTypeName;
            InterfaceFullTypeName = interfaceFullTypeName;
            CamelCaseTypeName = TypeName.ToCamelCase();
        }

        public virtual bool Equals(NameObject other)
        {
            return other.FullTypeName.Equals(FullTypeName)
                && other.InterfaceFullTypeName.Equals(InterfaceFullTypeName);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullTypeName, InterfaceFullTypeName);
        }
    }
}