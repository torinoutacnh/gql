using System.Reflection;
using gql.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using GraphQL;
using gql.Application.Gql.Types;
using gql.Application.Gql.Queries;
using gql.Application.Gql.Schemas;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        // Add gql
        services.AddGraphQL(b => {
            b.AddSystemTextJson()
            .AddDataLoader()
            .AddDocumentExecuter<DocumentExecuter>()
            .UseApolloTracing()
            .AddExecutionStrategySelector<StrategySelector>();

            b.Services.Register<TodoItemType>(serviceLifetime: GraphQL.DI.ServiceLifetime.Scoped);
            b.Services.Register<TodoListType>(serviceLifetime: GraphQL.DI.ServiceLifetime.Scoped);
            b.Services.Register<RootQuery>(serviceLifetime: GraphQL.DI.ServiceLifetime.Scoped);
            b.AddSchema<RootSchema>(serviceLifetime: GraphQL.DI.ServiceLifetime.Scoped);
        });

        return services;
    }
}
