using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class OrderItem : BaseAuditableEntity
{
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public int Price { get; set; }

    public string? Discount { get; set; }

    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }
}
