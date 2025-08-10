namespace social_blog_API.DTOs.Comments
{
    public class CreateCommentsDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
