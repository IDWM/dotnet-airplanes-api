using dotnet_airplanes_api.Src.DTOs.Auth;
using dotnet_airplanes_api.Src.Entities;
using dotnet_airplanes_api.Src.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_airplanes_api.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(UserManager<User> userManager, ITokenService tokenService)
        : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost("login")]
        public async Task<ActionResult<object>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
                return Unauthorized(new { error = "Credenciales inválidas." });

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result)
                return Unauthorized(new { error = "Credenciales inválidas." });

            var userName = user.UserName!;
            var token = await _tokenService.CreateTokenAsync(user.Id, userName);

            var auth = new AuthDto
            {
                UserId = user.Id,
                UserName = userName,
                Token = token
            };

            return Ok(auth);
        }
    }
}
