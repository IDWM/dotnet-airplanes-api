namespace dotnet_airplanes_api.Src.DTOs.Auth
{
    public class AuthDto
    {
        public required int UserId { get; set; }
        public required string UserName { get; set; }
        public required string Token { get; set; }
    }
}
