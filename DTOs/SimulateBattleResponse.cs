namespace HeroBattle.DTOs
{
    public class SimulateBattleResponse
    {
        public int Round { get; set; }
        public string Attacker { get; set; }
        public string Defender { get; set; }
        public string AttackerHealthChange { get; set; }
        public string DefenderHealthChange { get; set; }
        public string AttackerCurrentHealth { get; set; }
        public string DefenderCurrentHealth { get; set; }
    }
}
