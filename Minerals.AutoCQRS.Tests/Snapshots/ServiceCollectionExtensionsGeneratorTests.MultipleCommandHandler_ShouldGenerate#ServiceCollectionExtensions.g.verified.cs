﻿//HintName: ServiceCollectionExtensions.g.cs
// <auto-generated>
// This code was generated by a tool.
// Name: Minerals.AutoCQRS.Generators
// Version: {Removed}
// </auto-generated>
namespace Minerals.AutoCQRS
{
    using global::Microsoft.Extensions.DependencyInjection.Extensions;
    using global::Microsoft.Extensions.DependencyInjection;
    [global::System.Diagnostics.DebuggerNonUserCode]
    [global::System.Runtime.CompilerServices.CompilerGenerated]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static global::Microsoft.Extensions.DependencyInjection.IServiceCollection AddCommandsAndQueries(this global::Microsoft.Extensions.DependencyInjection.IServiceCollection collection, global::System.Action<global::Microsoft.Extensions.DependencyInjection.IServiceCollection, global::System.Type> injectPolicy = null)
        {
            if (injectPolicy is null)
            {
                injectPolicy = DefaultInjectPolicy;
            }
            collection.TryAddSingleton<global::Minerals.AutoCQRS.Interfaces.ICommandDispatcher, global::Minerals.AutoCQRS.CommandDispatcher>();
            collection.TryAddSingleton<global::Minerals.AutoCQRS.Interfaces.IQueryDispatcher, global::Minerals.AutoCQRS.QueryDispatcher>();
            injectPolicy.Invoke(collection, typeof(global::Examples.ExampleCommandHandler1));
            injectPolicy.Invoke(collection, typeof(global::TestNamespace2.ExampleCommandHandler2));
            injectPolicy.Invoke(collection, typeof(global::TestNamespace3.ExampleCommandHandler3));
            return collection;
        }

        private static void DefaultInjectPolicy(global::Microsoft.Extensions.DependencyInjection.IServiceCollection collection, global::System.Type serviceType)
        {
            collection.AddSingleton(serviceType);
        }
    }
}