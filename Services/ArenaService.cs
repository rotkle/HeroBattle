using HeroBattle.Helpers;
using HeroBattle.Models;
using HeroBattle.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeroBattle.Services
{
    public class ArenaService : IArenaService
    {
        private static readonly Random _random = new Random();
        private readonly DataContext _dbContext;
        public ArenaService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> GenerateArena(int numHero)
        {
            if (numHero <= 0)
            {
                throw new AppException("Number of Heroes must be greater than 0");
            }

            var arena = new Arena();
            arena.Id = Guid.NewGuid();

            // generate random N heroes
            arena.Heroes = Enumerable.Range(0, numHero).Select(x =>
            {
                var type = _random.Next(3) switch
                {
                    0 => HeroType.Archer,
                    1 => HeroType.Horseman,
                    2 => HeroType.Swordsman,
                    _ => throw new InvalidOperationException()
                };

                var initialHealth = type switch
                {
                    HeroType.Archer => 100,
                    HeroType.Horseman => 150,
                    HeroType.Swordsman => 120,
                    _ => throw new InvalidOperationException()
                };

                return new Hero { Health = initialHealth, MaxHealth = initialHealth, Type = type };
            }).ToList();

            _dbContext.Arenas.Add(arena);
            await _dbContext.SaveChangesAsync();

            return arena.Id;
        }

        public async Task<Arena> GetArena(Guid arenaId)
        {
            return await _dbContext.Arenas.Include(x => x.Heroes).FirstOrDefaultAsync(x => x.Id == arenaId);
        }
    }
}
