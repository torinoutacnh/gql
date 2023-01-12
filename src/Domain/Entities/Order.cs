using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class Order:BaseAuditableEntity
{
    public string? CustomerName { get; set; }

    public string? CustomerAddress { get; set; }

    public string? CustomerPhone { get; set; }

    public string? CustomerEmail { get; set; }

    public int OrderTotal { get; set; }

    public string? Note { get; set; }

    public Guid? ProfileId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }
}
