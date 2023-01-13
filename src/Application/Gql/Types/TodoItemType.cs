using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gql.Application.Common.Interfaces;
using gql.Application.Gql.Commons;
using gql.Domain.Entities;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace gql.Application.Gql.Types;

public class TodoItemType : ObjectGraphType<TodoItem>
{
    public TodoItemType(IApplicationDbContext dbcontext, IDataLoaderContextAccessor accessor)
    {
        Name = nameof(TodoItem);

        Field(i => i.Title).Description("Task title, required unique");

        Field(i => i.Id);
        Field(i => i.Created);
        Field(i => i.CreatedBy, nullable: true);
        Field(i => i.LastModified, nullable: true);
        Field(i => i.LastModifiedBy, nullable: true);
        Field(i => i.IsDeleted);

        Interface<BaseAuditableInterface>();

        Field<TodoListType, TodoList>("list")
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
