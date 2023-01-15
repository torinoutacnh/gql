using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gql.Application.Common.Interfaces;
using gql.Application.gql.Types;
using gql.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace gql.Application.gql.Queries;

public class RootQuery
{
    public IQueryable<TodoList> GetTodoLists([Service] IApplicationDbContext applicationDbContext) => applicationDbContext.Get<TodoList>().AsNoTracking();
    public IQueryable<TodoItem> GetTodoItems([Service] IApplicationDbContext applicationDbContext) => applicationDbContext.Get<TodoItem>().AsNoTracking();
}

public class RootQueryType : ObjectType<RootQuery>
{

    protected override void Configure(IObjectTypeDescriptor<RootQuery> descriptor)
    {
        descriptor
            .Field(f => f.GetTodoLists(default!))
            .Type<ListType<TodoListType>>()
            .UsePaging()
            .UseSorting()
            .UseFiltering();

        descriptor
            .Field(f => f.GetTodoItems(default!))
            .Type<ListType<TodoItemType>>()
            .UsePaging()
            .UseSorting()
            .UseFiltering();
    }
}
