using System.Reflection;
using gql.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using gql.Application.gql.Queries;
using gql.Application.gql.Types;
using gql.Application.Common.Interfaces;
using HotChocolate.Types.Pagination;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using HotChocolate.Execution.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IRequestExecutorBuilder AddCustomePersistQuery(this IRequestExecutorBuilder builder, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseRedis"))
        {
            var multiplexer = ConnectionMultiplexer
                .Connect(configuration["RedisSettings:DefaultConnection"] ?? throw new InvalidOperationException("Section RedisSettings:DefaultConnection not found!"));
            builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            builder.AddReadOnlyRedisQueryStorage(provider => provider.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
        }
        else
        {
            builder.Services.AddMemoryCache();
            builder.AddInMemoryQueryStorage();
        }

        return builder;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
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
            .AddAuthorization()
            .AddSorting()
            .AddFiltering()
            .AddProjections()
            .SetPagingOptions(new PagingOptions { MaxPageSize = 100, DefaultPageSize = 10, IncludeTotalCount = true })
            .AddQueryType<RootQueryType>()
            .AddType<TodoListType>()
            .AddApolloTracing()
            .UsePersistedQueryPipeline()   
            .AddCustomePersistQuery(configuration)
            .RegisterService<IApplicationDbContext>(ServiceKind.Synchronized);

        return services;
    }
}
