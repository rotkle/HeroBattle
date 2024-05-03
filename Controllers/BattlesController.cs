using HeroBattle.DTOs;
using HeroBattle.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HeroBattle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattlesController : ControllerBase
    {
        private readonly IBattleService _battleService;

        public BattlesController(IBattleService battleService)
        {
            _battleService = battleService;
        }

        [HttpGet]
        [Route("simulateBattle/{arenaId}")]
        public async Task<List<SimulateBattleResponse>> SimulateBattle(Guid arenaId)
        {
            return await _battleService.SimulateBattle(arenaId);
        }
    }
}