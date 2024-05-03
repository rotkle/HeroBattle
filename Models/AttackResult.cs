namespace HeroBattle.Models
{
    public class AttackResult
    {
        public AttackStatus AttackerStatus { get; set; }
        public AttackStatus DefenderStatus { get; set; }
        public int AttackerHealthChange { get; set; }
        public int DefenderHealthChange { get; set; }
        public int AttackerCurrentHealth { get; set; }
        public int DefenderCurrentHealth { get; set; }
    }

    public enum AttackStatus
    {
        Dies = 0,
        Blocked = 1,
        NoEffect = 2
    }
}
