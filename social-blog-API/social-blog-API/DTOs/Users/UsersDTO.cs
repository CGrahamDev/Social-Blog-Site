namespace social_blog_API.DTOs.Users
{
    public class UsersDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public int? AuthorId { get; set; }
    }
}
