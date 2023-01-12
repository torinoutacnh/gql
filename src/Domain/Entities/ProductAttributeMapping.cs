using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class ProductAttributeMapping:BaseAuditableEntity
{
    public string? Value { get; set; }

    public bool IsRequired { get; set; }

    public int DisplayOrder { get; set; }

    public Guid ProductId { get; set; }

    public Guid ProductAttributeId { get; set; }

    public int? Price { get; set; }

    public int? OldPrice { get; set; }

    public int? Stock { get; set; }

    public virtual Product Product { get; set; }

    public virtual ProductAttribute ProductAttribute { get; set; }
}
