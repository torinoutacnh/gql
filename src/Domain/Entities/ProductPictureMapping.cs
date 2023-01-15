using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class ProductPictureMapping:BaseAuditableEntity
{
    public string? Title { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsMainPicture { get; set; }

    public Guid ProductId { get; set; }

    public Guid PictureId { get; set; }

    public virtual Picture Picture { get; set; }

    public virtual Product Product { get; set; }
}
