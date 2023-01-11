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


        // Config field and can chain more
        Field(i => i.Id)
            .Description("TodoItem Id")
            .Name("id");
        Field(i => i.Title, nullable: true);
        Field(i => i.Note, nullable: true);
        Field(i => i.Priority);
        Field(i => i.Reminder, nullable: true);
        Field(i => i.Done);

        FieldAsync<TodoListType, TodoList>("list", resolve: ctx =>
        {
            var loader = accessor.Context
            .GetOrAddBatchLoader<Guid, TodoList>("GetTodoListByIds",
                GetTodoListByIds(dbcontext),
                l => l.Id,
                maxBatchSize: 5000);

            return loader.LoadAsync(ctx.Source.ListId).GetResultAsync();
        });
    }

    private static Func<IEnumerable<Guid>, Task<IEnumerable<TodoList>>> GetTodoListByIds(IApplicationDbContext dbcontext)
    {
        return (ids) => Task.FromResult(dbcontext.Get<TodoList>().Where(x => ids.Contains(x.Id)).AsNoTracking().AsEnumerable());
    }
}
