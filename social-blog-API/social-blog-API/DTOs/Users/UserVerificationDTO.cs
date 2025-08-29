using Azure.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace social_blog_API.DTOs.Users
{
    public class UserVerificationDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
