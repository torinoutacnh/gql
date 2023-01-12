using System;
using System.Collections.Generic;

namespace gql.Domain.Entities;

public class Comment : BaseAuditableEntity
{
    public Guid BlogId { get; set; }

    public Guid UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime DateCreate { get; set; }

    public bool IsBlog { get; set; }

    public Guid? CommentId { get; set; }

    public virtual Comment? CommentNavigation { get; set; }

    public virtual ICollection<Comment> InverseCommentNavigation { get; set; }
}
