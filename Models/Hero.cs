namespace HeroBattle.Models
{
    public class Hero
    {
        public int Id { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public HeroType Type { get; set; }
        public Arena Arena { get; set; }
    }
}
