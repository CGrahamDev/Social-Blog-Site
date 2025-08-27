namespace social_blog_API.DTOs.Users
{
    public class CheckUserVerificationDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsVerified { get; set; }
    }
}
