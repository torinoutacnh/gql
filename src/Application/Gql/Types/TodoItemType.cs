using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gql.Application.Common.Interfaces;
using gql.Domain.Entities;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace gql.Application.Gql.Types;

public class TodoItemType : ObjectGraphType<TodoItem>
{
    private readonly IApplicationDbContext _dbcontext;
    private readonly IDataLoaderContextAccessor _accessor;

    public TodoItemType(IApplicationDbContext dbcontext, IDataLoaderContextAccessor accessor)
    {
        _dbcontext = dbcontext;
        _accessor = accessor;


        Field(i => i.Id, nullable: true)
            .Description("TodoItem Id")
            .Name("id");
        Field(i => i.Title, nullable: true);
        Field(i => i.Note, nullable: true);
        Field(i => i.Priority, nullable: true);
        Field(i => i.Reminder, nullable: true);
        Field(i => i.Done, nullable: true);
        Field(i => i.Created, nullable: true);
        Field(i => i.CreatedBy, nullable: true);
        Field(i => i.LastModified, nullable: true);
        Field(i => i.LastModifiedBy, nullable: true);
        Field(i => i.IsDeleted, nullable: true);

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
