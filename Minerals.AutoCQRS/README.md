# Minerals.AutoCQRS

This NuGet package offers a lightweight and efficient way to implement the CQRS pattern in ASP.NET Core applications. It focuses on providing basic interfaces for commands, queries and pipelines, along with automatic dependency injection, without introducing the overhead of a mediator library such as MediatR.

## Features

- **Reduced boilerplate:** Package has dedicated interfaces for commands and queries, you don't need to write additional intermediate interfaces for code readability.
- **Pipelines:** Package provides easy to use pipelines for concrete commands and queries.
- **Automatic Dependency Injection:** Package provides automatic registration of all classes that implements *ICommandHandler*, *IQueryHandler*, *ICommandPipeline* or *IQueryPipeline* interfaces.
- **Performance:** Package does not use System.Reflection at all, dependency injection is resolved at compile time.
- **Compatible with .NET Standard 2.0 and C# 7.3+:** Works on a wide range of platforms and development environments.

## Installation

Add the Minerals.AutoCQRS nuget package to your C# project using the following methods:

### 1. Project file definition

```xml
<PackageReference Include="Minerals.AutoCQRS" Version="0.3.0" />
```

### 2. dotnet command

```bat
dotnet add package Minerals.AutoCQRS
```

## Usage

The examples below shows how to use the package and its components.

### ICommandHandler and IQueryHandler

Example implementation of command and query along with their handlers.

```csharp
namespace Minerals.Examples
{
    public class ExampleCommand : Minerals.AutoCQRS.Interfaces.ICommand
    {
        // ...
    }

    public class ExampleQuery : Minerals.AutoCQRS.Interfaces.IQuery
    {
        // ...
    }

    public class ExampleCommandHandler : Minerals.AutoCQRS.Interfaces.ICommandHandler<ExampleCommand, int>
    {
        public Task<int> Handle(ExampleCommand command, CancellationToken cancellation = default)
        {
            // ...
        }
    }

    public class ExampleQueryHandler : Minerals.AutoCQRS.Interfaces.IQueryHandler<ExampleQuery, int>
    {
        public Task<int> Handle(ExampleQuery query, CancellationToken cancellation = default)
        {
            // ...
        }
    }
}
```

### Dependency Registration

By default, dependencies are registered as Singleton.

```csharp
// Adding Handlers
builder.Services.AddAutoCQRSHandlers();

// Adding Pipelines
builder.Services.AddAutoCQRSPipelines();

// or

// You can set your own policy for dependency injection
builder.Services.AddAutoCQRSHandlers((collection, serviceInterfaceType, serviceConcreteType) => collection.AddScoped(serviceInterfaceType, serviceConcreteType));

builder.Services.AddAutoCQRSPipelines((collection, serviceInterfaceType, serviceConcreteType) => collection.AddScoped(serviceInterfaceType, serviceConcreteType));

```

### Pipelines

Below is an example of a pipeline configuration for a command with three handlers. A Class which implements the ``ICommandPipeline`` or ``IQueryPipeline`` interface must have the ``partial`` modifier.

```csharp
    public class TestCommand : ICommand
    {
        // ...
    }

    public class TestCommandHandler1 : ICommandHandler<TestCommand, string>
    {
        // ...
    }

    public class TestCommandHandler2 : ICommandHandler<TestCommand, string>
    {
        // ...
    }

    public class TestCommandHandler3 : ICommandHandler<TestCommand, string>
    {
        // ...
    }

    // That's all you need to create a pipeline for the selected command or query
    // Pipelines interfaces have overloads up to 8 handlers
    public partial class TestCommandPipeline : ICommandPipeline<TestCommand, string, TestCommandHandler1, TestCommandHandler2, TestCommandHandler3>;
```

### Dispatching

If one command or query has multiple handlers the last handler will be executed and returned.

```csharp
// Dispatching selected command (ICommandDispatcher)
string result = await _commandDispatcher.Dispatch<TestCommand, string>(new TestCommand());

// Dispatching selected query (IQueryDispatcher)
string result = await _queryDispatcher.Dispatch<TestQuery, string>(new TestQuery());

// Dispatching selected command pipeline (ICommandPipelineDispatcher)
IReadOnlyList<string> results = await _commandPipelineDispatcher.Dispatch<TestCommand, string>(new TestCommand());

// Dispatching selected query pipeline (IQueryPipelineDispatcher)
IReadOnlyList<string> results = await _queryPipelineDispatcher.Dispatch<TestQuery, string>(new TestQuery());
```

### Custom Dispatchers

You can write your own implementation of a command, query or pipeline dispatcher using the appropriate interface, ``ICommandDispatcher``, ``IQueryDispatcher``, ``ICommandPipelineDispatcher`` or ``IQueryPipelineDispatcher``.

```csharp
public class CustomCommandDispatcher : ICommandDispatcher
{
    public Task<TResult> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellation = default)
        where TCommand : ICommand, new()
        where TResult : notnull
    {
        // ...
    }
}

// or

public class CommandPipelineDispatcher : ICommandPipelineDispatcher
{
    public async Task<IReadOnlyList<TResult>> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellation = default)
        where TCommand : ICommand, new()
        where TResult : notnull
    {
        // ...
    }
}
```

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [branches on this repository](https://github.com/SzymonHalucha/Minerals.AutoCQRS/branches).

## Authors

- **Szymon Hałucha** - Maintainer

See also the list of [contributors](https://github.com/SzymonHalucha/Minerals.AutoCQRS/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for details.
