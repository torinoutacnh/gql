using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class Blog : BaseAuditableEntity
{
    public string? Title { get; set; }

    public string? TitleEng { get; set; }

    public string? Slug { get; set; }

    public string? BlogImage { get; set; }

    public string? Description { get; set; }

    public string? Content { get; set; }

    public string? DescriptionEng { get; set; }

    public string? ContentEng { get; set; }

    public string? MetaKeywords { get; set; }

    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public bool IsAvailable { get; set; }

    public bool IsHomePage { get; set; }

    public Guid PictureId { get; set; }

    public Guid BlogCategoryId { get; set; }

    public int Position { get; set; }

    public string? ContentTwo { get; set; }

    public string? Link { get; set; }

    public string? ImageUrl { get; set; }

    public string? BlogParentId { get; set; }

    public virtual BlogCategory BlogCategory { get; set; }

    public virtual ICollection<BlogTagMapping> BlogTagMappings { get; set; }
}
