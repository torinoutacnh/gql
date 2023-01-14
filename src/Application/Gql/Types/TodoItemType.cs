using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gql.Application.Common.Interfaces;
using gql.Application.Gql.Commons;
using gql.Domain.Entities;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace gql.Application.Gql.Types;

public class TodoItemType : AutoRegisteringObjectGraphType<TodoItem>
{
    public TodoItemType(IApplicationDbContext dbcontext, IDataLoaderContextAccessor accessor):base(x => x.DomainEvents)
    {
        Name = "TodoItemEntity";

        Interface<BaseAuditableInterface>();

        Field<TodoListType, TodoList>("TodoListOfTodoItem")
            .ResolveAsync( context =>
            {
                var loader = accessor.Context?
                .GetOrAddBatchLoader<Guid, TodoList>("GetTodoListByIds",
                    GetTodoListByIds(dbcontext),
                    l => l.Id,
                    maxBatchSize: 5000);

                return loader.LoadAsync(context.Source.ListId).GetResultAsync();
            });
    }

    private static Func<IEnumerable<Guid>, Task<IEnumerable<TodoList>>> GetTodoListByIds(IApplicationDbContext dbcontext)
    {
        return (ids) => Task.FromResult(dbcontext.Get<TodoList>().Where(x => ids.Contains(x.Id)).AsNoTracking().AsEnumerable());
    }
}
