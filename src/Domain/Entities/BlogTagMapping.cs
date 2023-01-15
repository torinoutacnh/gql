using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class BlogTagMapping : BaseAuditableEntity
{
    public Guid BlogId { get; set; }

    public Guid TagId { get; set; }

    public virtual Blog Blog { get; set; }

    public virtual Tag Tag { get; set; }
}
