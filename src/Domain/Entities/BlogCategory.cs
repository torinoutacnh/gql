using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class BlogCategory : BaseAuditableEntity
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool Status { get; set; }

    public string? Slug { get; set; }

    public int Layout { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsStaticPage { get; set; }

    public Guid? CategoryParentId { get; set; }

    public virtual BlogCategory? CategoryParent { get; set; }

    public virtual ICollection<BlogCategory> InverseCategoryParent { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; }
}
