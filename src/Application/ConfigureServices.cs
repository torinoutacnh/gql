using System.Reflection;
using gql.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using gql.Application.gql.Queries;
using gql.Application.gql.Types;
using gql.Application.Common.Interfaces;
using HotChocolate.Types.Pagination;

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

        services
            .AddGraphQLServer()
            .AddSorting()
            .AddFiltering()
            .AddProjections()
            .SetPagingOptions(new PagingOptions { MaxPageSize = 100, DefaultPageSize = 10, IncludeTotalCount = true })
            .AddQueryType<RootQueryType>()
            .AddType<TodoListType>()
            .AddApolloTracing()
            .RegisterService<IApplicationDbContext>(ServiceKind.Synchronized);

        return services;
    }
}
