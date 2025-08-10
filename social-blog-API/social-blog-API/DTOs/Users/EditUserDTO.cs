using System.Reflection.Metadata;

namespace social_blog_API.DTOs.Users
{
    public class EditUserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
    }
}
