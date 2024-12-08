using System.ComponentModel.DataAnnotations;

namespace dotnet_airplanes_api.Src.DTOs.Auth
{
    public class LoginDto
    {
        [EmailAddress]
        public required string Email { get; set; }

        [StringLength(20, MinimumLength = 8)]
        public required string Password { get; set; }
    }
}
