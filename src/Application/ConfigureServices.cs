using System.Reflection;
using gql.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using GraphQL;
using gql.Application.Gql.Types;
using gql.Application.Gql.Queries;
using gql.Application.Gql.Schemas;
using gql.Application.Gql.Commons;
using gql.Domain.Common;
using gql.Application.Utils;
using GraphQL.Types;
using GraphQL.DI;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using gql.Application.Gql.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IGraphQLBuilder AddGqlInterfaceDict(this IGraphQLBuilder builder)
    {
        var types = ReflectionUtils.GetTypeImplementedT<BaseAuditableEntity>();
        var dict = new Dictionary<Type, Type>();

        foreach (var typeEntity in types)
        {
            var gen = typeof(ObjectGraphType<>);
            var tGrapth = gen.MakeGenericType(typeEntity);

            if (typeEntity is not null)
                dict.Add(tGrapth, typeEntity);
        }

        builder.Services.Register<IAuditableEntityResolver>(new AuditableEntityResolver(dict));

        return builder;
    }

    public static IGraphQLBuilder AddPaginatedList(this IGraphQLBuilder builder)
    {
        builder.Services.Register(typeof(PaginatedListType<>), typeof(PaginatedListType<>), GraphQL.DI.ServiceLifetime.Scoped);
        builder.Services.TryRegister(typeof(AutoRegisteringObjectGraphType<>), typeof(AutoRegisteringObjectGraphType<>), GraphQL.DI.ServiceLifetime.Scoped);
        builder.Services.TryRegister(typeof(ObjectGraphType<>), typeof(ObjectGraphType<>), GraphQL.DI.ServiceLifetime.Scoped);
        builder.AddClrTypeMappings();
        return builder;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        // kestrel
        services.Configure<KestrelServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });

        // IIS
        services.Configure<IISServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });

        // Add gql
        services.AddGraphQL(b => {
            b.AddSystemTextJson()
            .AddDataLoader()
            .AddDocumentExecuter<DocumentExecuter>()
            .UseApolloTracing()
            .AddExecutionStrategySelector<StrategySelector>();

            b.AddGqlInterfaceDict();
            b.AddPaginatedList();

            b.Services.Register<PageModelInputType>(serviceLifetime: GraphQL.DI.ServiceLifetime.Scoped, replace: true);
            b.Services.Register<BaseAuditableInterface>(serviceLifetime: GraphQL.DI.ServiceLifetime.Scoped, replace: true);
            b.Services.Register<BaseEventType>(serviceLifetime: GraphQL.DI.ServiceLifetime.Scoped, replace: true);
            b.Services.Register<TodoItemType>(serviceLifetime: GraphQL.DI.ServiceLifetime.Scoped, replace: true);
            b.Services.Register<TodoListType>(serviceLifetime: GraphQL.DI.ServiceLifetime.Scoped, replace: true);
            b.Services.Register<RootQuery>(serviceLifetime: GraphQL.DI.ServiceLifetime.Scoped, replace: true);
            b.AddSchema<RootSchema>(serviceLifetime: GraphQL.DI.ServiceLifetime.Scoped);
            //b.AddComplexityAnalyzer(opt =>
            //{
            //    opt.MaxDepth = 15;
            //    opt.MaxComplexity = 2000;
            //    //opt.FieldImpact = 100;
            //});

            // Enable debug mode for the schema
            b.ConfigureExecutionOptions(opt =>
            {
                opt.ThrowOnUnhandledException = true;
            });
        });

        return services;
    }
}
