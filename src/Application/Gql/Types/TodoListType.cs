using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gql.Application.Common.Interfaces;
using gql.Application.Gql.Commons;
using gql.Domain.Common;
using gql.Domain.Entities;
using gql.Domain.ValueObjects;
using GraphQL.DataLoader;
using GraphQL.Reflection;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace gql.Application.Gql.Types;

public class TodoListType: AutoRegisteringObjectGraphType<TodoList>
{
    public TodoListType(IApplicationDbContext dbcontext, IDataLoaderContextAccessor accessor) : base(x=>x.Colour, x=>x.DomainEvents)
    {
        Name = "TodoListEntity";

        Interface<BaseAuditableInterface>();

        Field<ListGraphType<TodoItemType>, IEnumerable<TodoItem>>("TodoListItems")
            .ResolveAsync(context =>
            {
                var loader = accessor.Context?.GetOrAddCollectionBatchLoader("GetTodoItemsByTodoListId",
                                                GetTodoItemsByTodoListId(dbcontext),
                                                i => i.ListId,
                                                maxBatchSize: 5000);

                return loader.LoadAsync(context.Source.Id).GetResultAsync();
            });
    }

    private static Func<IEnumerable<Guid>, Task<IEnumerable<TodoItem>>> GetTodoItemsByTodoListId(IApplicationDbContext dbcontext)
    {
        return (ids) => Task.FromResult(dbcontext.Get<TodoItem>().Where(x => ids.Contains(x.ListId)).AsNoTracking().AsEnumerable());
    }
}
