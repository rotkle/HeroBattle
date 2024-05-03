using HeroBattle.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HeroBattle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArenasController : ControllerBase
    {
        private readonly IArenaService _arenaService;

        public ArenasController(IArenaService arenaService)
        {
            _arenaService = arenaService;
        }

        [HttpPost]
        [Route("createArena")]
        public async Task<Guid> CreateArena([FromQuery] int numHero)
        {
            return await _arenaService.GenerateArena(numHero);
        }
    }
}
