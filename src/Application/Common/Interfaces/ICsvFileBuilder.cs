using gql.Application.TodoLists.Queries.ExportTodos;

namespace gql.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
