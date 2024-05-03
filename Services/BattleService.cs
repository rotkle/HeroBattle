using HeroBattle.DTOs;
using HeroBattle.Models;
using HeroBattle.Services.Interfaces;

namespace HeroBattle.Services
{
    public class BattleService : IBattleService
    {
        private readonly IArenaService _arenaService;

        public BattleService(IArenaService arenaService)
        {
            _arenaService = arenaService;
        }

        public async Task<List<SimulateBattleResponse>> SimulateBattle(Guid arenaId)
        {
            var arena = await _arenaService.GetArena(arenaId);
            if (arena == null)
            {
                throw new KeyNotFoundException("Arena is not found!");
            }

            var battle = new Battle(arena);
            while (battle.IsContinue)
            {
                battle.IncreaseRound();
                var attacker = battle.GetRandomHero();
                var defender = battle.GetRandomHero(attacker);
                // calculate attack, health and status of attacker and defender
                var result = battle.Attack(attacker, defender);
                // update health for heroes after attack
                battle.UpdateHealth(attacker, defender, result);
                battle.AddRoundToHistory(attacker, defender, result);
            }

            // convert battle history to response
            var response = new List<SimulateBattleResponse>();
            foreach (var round in battle.History)
            {
                response.Add(new SimulateBattleResponse
                {
                    Round = round.Round,
                    Attacker = $"{round.Attacker.Type.ToString()} {round.Attacker.Id}",
                    Defender = $"{round.Defender.Type.ToString()} {round.Defender.Id}",
                    AttackerHealthChange = $"-{round.Result.AttackerHealthChange}",
                    DefenderHealthChange = $"-{round.Result.DefenderHealthChange}",
                    AttackerCurrentHealth = $"{round.Result.AttackerCurrentHealth}",
                    DefenderCurrentHealth = $"{round.Result.DefenderCurrentHealth}"
                });
            }

            return response;
        }
    }
}
