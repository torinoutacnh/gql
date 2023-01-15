using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class Product:BaseAuditableEntity
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Slug { get; set; }

    public string? Content { get; set; }

    public int Price { get; set; }

    public int OldPrice { get; set; }

    public string? MetaKeywords { get; set; }

    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public string? NameEng { get; set; }

    public string? DescEng { get; set; }

    public string? ContentEng { get; set; }

    public Guid ProductCategoryId { get; set; }

    public bool IsHomePage { get; set; }

    public bool IsPublic { get; set; }

    public int Position { get; set; }

    public int? Stock { get; set; }

    public int? LikeProduct { get; set; }

    public string? Tags { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }

    public virtual ICollection<ProductAttributeMapping> ProductAttributeMappings { get; set; }

    public virtual ProductCategory ProductCategory { get; set; }

    public virtual ICollection<ProductPictureMapping> ProductPictureMappings { get; set; }
}
