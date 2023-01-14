using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gql.Application.Common.Interfaces;
using gql.Application.Common.Models;
using gql.Application.Gql.Models;
using gql.Domain.Common;
using gql.Domain.Entities;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace gql.Application.Gql.Types;

public class PaginatedListType<T> : ObjectGraphType<PaginatedList<T>> where T : BaseAuditableEntity
{
    public PaginatedListType(IApplicationDbContext dbContext)
    {
        Name = "Page";

        Field<ListGraphType<AutoRegisteringObjectGraphType<T>>, IEnumerable<T>>("items")
            .Resolve(context =>
            {
                return dbContext.Get<T>().AsNoTracking().AsEnumerable();
            });
        Field(i => i.TotalPages);
        Field(i => i.HasNextPage);
        Field(i => i.HasPreviousPage);
        Field(i => i.PageNumber);
        Field(i => i.TotalCount);
    }
}
