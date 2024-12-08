using dotnet_airplanes_api.Src.Data;
using dotnet_airplanes_api.Src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_airplanes_api.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AirplaneController(DataContext dataContext) : ControllerBase
    {
        private readonly DataContext _dataContext = dataContext;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airplane>>> GetAirplanes()
        {
            var airplanes = await _dataContext.Airplanes.ToListAsync();

            return Ok(airplanes);
        }
    }
}
