using gql.Application.Common.Mappings;
using gql.Domain.Entities;

namespace gql.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
