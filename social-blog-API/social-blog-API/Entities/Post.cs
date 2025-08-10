using System;
using System.Collections.Generic;

namespace social_blog_API.Entities;

public partial class Post
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public int AuthorId { get; set; }

    public virtual User Author { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
