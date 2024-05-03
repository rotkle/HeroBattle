using HeroBattle.DTOs;

namespace HeroBattle.Services.Interfaces
{
    public interface IBattleService
    {
        Task<List<SimulateBattleResponse>> SimulateBattle(Guid arenaId);
    }
}
