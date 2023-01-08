using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gql.Application.Common.Interfaces;
using gql.Domain.Entities;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace gql.Application.Gql.Queries;

public class TodoItemInterface : InterfaceGraphType<TodoItem>
{
    public TodoItemInterface() {
        Name = "todo-item";
    }
}

public class Query1: ObjectGraphType<object>
{
    public Query1(IServiceProvider serviceProvider)
    {
        Name = "Query1";

        var dbContext = serviceProvider.GetRequiredService<IApplicationDbContext>();

        FieldAsync<TodoItemInterface>("todo-items", 
            resolve: async context => dbContext.TodoItems.ToListAsync());
    }
}
