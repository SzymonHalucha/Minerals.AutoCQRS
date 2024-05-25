namespace Minerals.AutoCQRS
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class CommandPipelineItemAttribute : Attribute;
}