using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gql.Application.Common.Interfaces;
using gql.Domain.Entities;
using GraphQL.DataLoader;
using GraphQL.Reflection;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace gql.Application.Gql.Types;

public class TodoListType: ObjectGraphType<TodoList>
{
    public TodoListType(IApplicationDbContext dbcontext, IDataLoaderContextAccessor accessor) 
    {
        Field(i => i.Id, nullable: true)
            .Description("TodoItem Id")
            .Name("id");
        Field(i => i.Title, nullable: true);
        Field(i => i.Created, nullable: true);
        Field(i => i.CreatedBy, nullable: true);
        Field(i => i.LastModified, nullable: true);
        Field(i => i.LastModifiedBy, nullable: true);
        Field(i => i.IsDeleted, nullable: true);

        Field<ListGraphType<TodoItemType>, IEnumerable<TodoItem>>("items")
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
