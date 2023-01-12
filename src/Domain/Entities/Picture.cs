using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class Picture:BaseAuditableEntity
{
    public string? Binary { get; set; }

    public string? MimeType { get; set; }

    public string? Url { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ProductPictureMapping> ProductPictureMappings { get; set; }
}
