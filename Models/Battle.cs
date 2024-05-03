namespace HeroBattle.Models
{
    public class Battle
    {
        private static readonly Random _random = new Random();
        public Battle(Arena arena)
        {
            Heroes = arena.Heroes;
            Round = 0;
            History = new List<BattleRound>();
        }

        public List<Hero> Heroes { get; }
        public int Round { get; private set; }
        public List<BattleRound> History { get; }

        public bool IsContinue => Heroes.Count > 1;

        public AttackResult Attack(Hero attacker, Hero defender)
        {
            var attackResult = new AttackResult();
            // attacker and defender health is halved for participating in battle
            attackResult.AttackerHealthChange = attacker.Health / 2;
            attackResult.DefenderHealthChange = defender.Health / 2;

            switch (attacker.Type)
            {
                case HeroType.Archer:

                    switch (defender.Type)
                    {
                        case HeroType.Archer: 
                        case HeroType.Swordsman:

                            attackResult.AttackerStatus = AttackStatus.NoEffect;
                            attackResult.DefenderStatus = AttackStatus.Dies;
                            // defender is dies and lost all the current health
                            attackResult.DefenderHealthChange = defender.Health;

                            break;
                        case HeroType.Horseman:

                            var isBlocked = _random.NextDouble() > 0.4;
                            if (isBlocked)
                            {
                                attackResult.AttackerStatus = AttackStatus.NoEffect;
                                attackResult.DefenderStatus = AttackStatus.Blocked;
                            }
                            else
                            {
                                attackResult.AttackerStatus = AttackStatus.NoEffect;
                                attackResult.DefenderStatus = AttackStatus.Dies;
                                attackResult.DefenderHealthChange = defender.Health;
                            }

                            break;
                    }

                    break;
                case HeroType.Horseman:

                    switch (defender.Type)
                    {
                        case HeroType.Archer:
                        case HeroType.Horseman:

                            attackResult.AttackerStatus = AttackStatus.NoEffect;
                            attackResult.DefenderStatus = AttackStatus.Dies;
                            attackResult.DefenderHealthChange = defender.Health;

                            break;
                        case HeroType.Swordsman:

                            attackResult.AttackerStatus = AttackStatus.Dies;
                            attackResult.DefenderStatus = AttackStatus.NoEffect;
                            attackResult.AttackerHealthChange = attacker.Health;

                            break;
                    }

                    break;
                case HeroType.Swordsman:

                    switch (defender.Type)
                    {
                        case HeroType.Swordsman:
                        case HeroType.Archer:

                            attackResult.AttackerStatus = AttackStatus.NoEffect;
                            attackResult.DefenderStatus = AttackStatus.Dies;
                            attackResult.DefenderHealthChange = defender.Health;

                            break;
                        case HeroType.Horseman:

                            attackResult.AttackerStatus = AttackStatus.NoEffect;
                            attackResult.DefenderStatus = AttackStatus.NoEffect;

                            break;
                    }

                    break;
            }

            return attackResult;
        }

        public void UpdateHealth(Hero attacker, Hero defender, AttackResult result)
        {
            attacker.Health = attacker.Health - result.AttackerHealthChange;
            defender.Health = defender.Health - result.DefenderHealthChange;
            result.AttackerCurrentHealth = attacker.Health;
            result.DefenderCurrentHealth = defender.Health;

            // update all heroes health who is not involved to battle
            foreach (var hero in Heroes)
            {
                if (hero.Id != attacker.Id && hero.Id != defender.Id)
                {
                    hero.Health = Math.Min(hero.MaxHealth, hero.Health + 10);
                }
            }

            // if the current health of hero who participating in battle is less than a quarter of initial heath -> they dies
            // or if hero status after attack is dies then they dies
            if (attacker.Health < (attacker.MaxHealth / 4) || result.AttackerStatus == AttackStatus.Dies)
            {
                Heroes.Remove(attacker);
            }

            if (defender.Health < (defender.MaxHealth / 4) || result.DefenderStatus == AttackStatus.Dies)
            {
                Heroes.Remove(defender);
            }
        }

        public void IncreaseRound()
        {
            Round++;
        }

        public void AddRoundToHistory(Hero attacker, Hero defender, AttackResult result)
        {
            History.Add(new BattleRound(Round, attacker, defender, result));
        }

        public Hero GetRandomHero(Hero exclude = null)
        {
            var hero = Heroes[_random.Next(Heroes.Count)];

            while (exclude != null && hero.Id == exclude.Id)
            {
                hero = Heroes[_random.Next(Heroes.Count)];
            }

            return hero;
        }
    }
}
