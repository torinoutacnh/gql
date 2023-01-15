using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class Tag : BaseAuditableEntity
{
    public string? Name { get; set; }

    public string? Slug { get; set; }

    public bool? IsBlog { get; set; }

    public virtual ICollection<BlogTagMapping> BlogTagMappings { get; set; }
}
