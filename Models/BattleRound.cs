namespace HeroBattle.Models
{
    public class BattleRound
    {
        public BattleRound(int round, Hero attacker, Hero defender, AttackResult result)
        {
            Round = round;
            Attacker = attacker;
            Defender = defender;
            Result = result;
        }
        public int Round { get; set; }
        public Hero Attacker { get; set; }
        public Hero Defender { get; set; }
        public AttackResult Result { get; set; }
    }
}
