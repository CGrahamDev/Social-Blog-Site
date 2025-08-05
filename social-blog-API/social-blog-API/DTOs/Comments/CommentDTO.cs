using Microsoft.Data.SqlClient.DataClassification;

namespace social_blog_API.DTOs.Comments
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
    }
}
