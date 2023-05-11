using System.ComponentModel.DataAnnotations;

namespace AuthorizationApi.Models
{
    public class Login
    {
        [Required]
        public string Username { get; init; }
        [Required]
        public string Password { get; init; }
    }
}
