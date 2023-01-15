using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class WebsiteAttribute:BaseAuditableEntity
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ControlType { get; set; }

    public string? Type { get; set; }

    public string? Value { get; set; }

    public bool IsPublic { get; set; }
}
