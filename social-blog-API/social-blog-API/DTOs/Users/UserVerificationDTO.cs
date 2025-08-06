using Azure.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace social_blog_API.DTOs.Users
{
    public class UserVerificationDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public bool IsVerified { get; set; } = false;
    }
}
