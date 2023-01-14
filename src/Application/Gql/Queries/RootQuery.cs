using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gql.Application.Common.Interfaces;
using gql.Application.Common.Models;
using gql.Application.Gql.Models;
using gql.Application.Gql.Types;
using gql.Domain.Entities;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace gql.Application.Gql.Queries;

public class RootQuery: ObjectGraphType
{
    public RootQuery(IApplicationDbContext dbContext)
    {
        Name = "Root";

        //Field<ListGraphType<TodoItemType>, List<TodoItem>>("TodoItems")
        //    .ResolveAsync(context =>
        //    {
        //        return dbContext.Get<TodoItem>().AsNoTracking().ToListAsync();
        //    });

        Field<PaginatedListType<TodoItem>, PaginatedList<TodoItem>>("TodoItems")
            .Argument<PageModelInputType>("pageModel")
            .ResolveAsync(context =>
            {
                var model = context.GetArgument<PageModel>("pageModel");

                return PaginatedList<TodoItem>.CreateAsync(dbContext.Get<TodoItem>().AsNoTracking(), model.Page, model.Size);
            });

        Field<ListGraphType<TodoItemType>, List<TodoItem>>("TodoItemById")
            .Argument<IdGraphType>("itemId")
            .ResolveAsync(context =>
            {
                var id = context.GetArgument<Guid>("itemId");
                return dbContext.Get<TodoItem>().AsNoTracking().Where(x => x.Id == id).ToListAsync();
            });

        Field<ListGraphType<TodoListType>, List<TodoList>>("TodoLists")
            .ResolveAsync(context => dbContext.Get<TodoList>().ToListAsync());
    }
}
