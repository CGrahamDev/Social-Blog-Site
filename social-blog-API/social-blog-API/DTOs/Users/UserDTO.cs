using social_blog_API.Entities;

namespace social_blog_API.DTOs.Users
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string? Password { get; set; }

        public string? Description { get; set; }
    }
}
