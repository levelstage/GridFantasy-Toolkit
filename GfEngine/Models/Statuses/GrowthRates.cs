namespace GfEngine.Models.Statuses
{
    public class GrowthRates
    {
        public int HpRate { get; set; }
        public int AttackRate { get; set; }
        public int MagicAttackRate { get; set; }
        public int DefenseRate { get; set; }
        public int MagicDefenseRate { get; set; }
        public int AgilityRate { get; set; }

        public GrowthRates()
        {
            HpRate = 0;
            AttackRate = 0;
            MagicAttackRate = 0;
            DefenseRate = 0;
            MagicDefenseRate = 0;
            AgilityRate = 0;
        }

        public GrowthRates(int hpRate, int attackRate, int magicAttackRate, int defenseRate, int magicDefenseRate, int agilityRate)
        {
            HpRate = hpRate;
            AttackRate = attackRate;
            MagicAttackRate = magicAttackRate;
            DefenseRate = defenseRate;
            MagicDefenseRate = magicDefenseRate;
            AgilityRate = agilityRate;
        }
    }
}