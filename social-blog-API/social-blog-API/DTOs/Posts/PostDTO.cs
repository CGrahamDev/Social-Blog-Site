namespace social_blog_API.DTOs.Posts
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public int? AuthorId { get; set; }

        public int? Comments { get; set; }
    }
}
