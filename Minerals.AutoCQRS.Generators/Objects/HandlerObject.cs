namespace Minerals.AutoCQRS.Generators.Objects
{
    public readonly struct HandlerObject(string fullTypeName, string interfaceFullTypeName) : IEquatable<HandlerObject>
    {
        public string FullTypeName { get; } = fullTypeName;
        public string InterfaceFullTypeName { get; } = interfaceFullTypeName;

        public bool Equals(HandlerObject other)
        {
            return other.FullTypeName.Equals(FullTypeName)
                && other.InterfaceFullTypeName.Equals(InterfaceFullTypeName);
        }

        public override bool Equals(object? obj)
        {
            return obj is HandlerObject other
                && other.FullTypeName.Equals(FullTypeName)
                && other.InterfaceFullTypeName.Equals(InterfaceFullTypeName);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullTypeName, InterfaceFullTypeName);
        }
    }
}