using HeroBattle.Models;

namespace HeroBattle.Services.Interfaces
{
    public interface IArenaService
    {
        Task<Guid> GenerateArena(int numHero);
        Task<Arena> GetArena(Guid arenaId);
    }
}
