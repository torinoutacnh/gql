using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gql.Application.Common.Interfaces;
using gql.Application.Gql.Types;
using gql.Domain.Entities;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace gql.Application.Gql.Queries;

public class RootQuery: ObjectGraphType
{
    public RootQuery(IApplicationDbContext dbContext)
    {
        Name = "Root";

        FieldAsync<ListGraphType<TodoItemType>, List<TodoItem>>("TodoItems", 
            resolve: context => dbContext.Get<TodoItem>().ToListAsync());

        FieldAsync<ListGraphType<TodoListType>, List<TodoList>>("TodoLists",
            resolve: context => dbContext.Get<TodoList>().ToListAsync());
    }
}
