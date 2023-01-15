using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class ProductCategory:BaseAuditableEntity
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int Position { get; set; }

    public string? Notes { get; set; }

    public string? Slug { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}
