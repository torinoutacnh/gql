using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class ProductAttribute:BaseAuditableEntity
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ControlType { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<ProductAttributeMapping> ProductAttributeMappings { get; set; }
}
