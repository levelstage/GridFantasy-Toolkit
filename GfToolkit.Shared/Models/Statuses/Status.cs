namespace GfToolkit.Shared.Models.Statuses
{
    public class Status
    {
        public int MaxHp { get; set; }
        public int Defense { get; set; }
        public int MagicDefense { get; set; }
        public int Attack { get; set; }
        public int MagicAttack { get; set; }
        public int Agility { get; set; }

        public Status(int maxHp = 10, int defense = 5, int magicDefense = 5, int attack = 5, int magicAttack = 5, int agility = 5)
        {
            this.MaxHp = maxHp;
            this.Defense = defense;
            this.MagicDefense = magicDefense;
            this.Attack = attack;
            this.MagicAttack = magicAttack;
            this.Agility = agility;
        }

        public Status(Status status)
        {
            this.MaxHp = status.MaxHp;
            this.Defense = status.Defense;
            this.MagicDefense = status.MagicDefense;
            this.Attack = status.Attack;
            this.MagicAttack = status.MagicAttack;
            this.Agility = status.Agility;
        }
    }
}