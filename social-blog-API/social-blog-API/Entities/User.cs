using System;
using System.Collections.Generic;

namespace social_blog_API.Entities;

public partial class User
{
    public int Id { get; set; }

    public string DisplayName { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
