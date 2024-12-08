namespace dotnet_airplanes_api.Src.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(int userId, string userEmail);
    }
}
