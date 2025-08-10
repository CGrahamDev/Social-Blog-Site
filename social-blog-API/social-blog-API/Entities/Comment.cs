using System;
using System.Collections.Generic;

namespace social_blog_API.Entities;

public partial class Comment
{
    public int Id { get; set; }

    public string Content { get; set; }

    public int Likes { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public virtual Post Post { get; set; }

    public virtual User User { get; set; }
}
