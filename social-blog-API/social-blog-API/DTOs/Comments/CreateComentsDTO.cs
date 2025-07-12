namespace social_blog_API.DTOs.Comments
{
    public class CreateComentsDTO
    {
        public string Content { get; set; }
        public int Likes { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
    }
}
