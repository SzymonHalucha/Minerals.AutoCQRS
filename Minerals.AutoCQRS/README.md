# Minerals.AutoCQRS

*WORK IN PROGRESS...*

This NuGet package offers a lightweight and efficient way to implement the CQRS pattern in ASP.NET Core applications. It focuses on providing basic interfaces for commands and queries, along with automatic dependency injection, without introducing the overhead of a mediator library such as MediatR.

## Features

- **Reduced boilerplate:** Package has dedicated interfaces for commands and queries, you don't need to write additional intermediate interfaces for code readability.
- **Automatic Dependency Injection:** Package provides automatic registration of all classes that implements ICommandHandler<,> or IQueryHandler<,> interfaces.
- **Performance:** Package does not use System.Reflection at all, dependency injection is resolved at compile time.
- **Compatible with .NET Standard 2.0 and C# 7.3+:** Works on a wide range of platforms and development environments.

## Installation

Add the Minerals.AutoCQRS nuget package to your C# project using the following methods:

### 1. Project file definition

```xml
<PackageReference Include="Minerals.AutoCQRS" Version="0.1.0" />
```

### 2. dotnet command

```bat
dotnet add package Minerals.AutoCQRS
```

## Usage

Example of command and query usage along with their handlers:

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
        public Task<int> Handle(ExampleCommand command, CancellationToken cancellation)
        {
            // ...
        }
    }

    public class ExampleQueryHandler : Minerals.AutoCQRS.Interfaces.IQueryHandler<ExampleQuery, int>
    {
        public Task<int> Handle(ExampleQuery query, CancellationToken cancellation)
        {
            // ...
        }
    }
}
```

### Dependency Registration

```csharp
// By default, dependencies are registered as Singleton
builder.Services.AddCommandsAndQueries();

// or

// You can set your own policy for dependency injection
builder.Services.AddCommandsAndQueries((collection, serviceType) => collection.AddScoped(serviceType));
```

### Dispatchers

You can write your own implementation of a command or query dispatcher using the appropriate interface, ``ICommandDispatcher`` or ``IQueryDispatcher``:

```csharp
public class CustomCommandDispatcher : ICommandDispatcher
{
    public Task<TResult> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellation) where TCommand : ICommand, new()
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
