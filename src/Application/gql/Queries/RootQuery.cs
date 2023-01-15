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
    //[UsePaging(MaxPageSize = 100, IncludeTotalCount = true, DefaultPageSize = 10)]
    public IQueryable<TodoList> GetTodoLists([Service] IApplicationDbContext applicationDbContext) => applicationDbContext.Get<TodoList>().AsNoTracking();
}

public class RootQueryType : ObjectType<RootQuery>
{

    protected override void Configure(IObjectTypeDescriptor<RootQuery> descriptor)
    {
        descriptor
            .Field(f => f.GetTodoLists(default!))
            .Type<ListType<TodoListType>>()
            .UseSorting()
            .UsePaging()
            .UseFiltering();
    }
}
