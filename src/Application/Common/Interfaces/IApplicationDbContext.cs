using gql.Domain.Common;
using gql.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace gql.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<T> Get<T>() where T : BaseAuditableEntity;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
