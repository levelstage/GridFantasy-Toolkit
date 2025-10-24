using GfEngine.Battles.Units;

namespace GfEngine.Battles.Conditions
{
    public class TargetHpBelowCondition : ICondition
    {
        public int Constant { get; set; }
        public float Coefficient { get; set; }
        public TargetHpBelowCondition(int constant = 0, float coefficient = 0f)
        {
            Constant = constant;
            Coefficient = coefficient;
        }
        public bool IsMet(BattleContext battleContext)
        {
            if (battleContext.Target.Occupant == null) return false;
            Unit target = battleContext.Target.Occupant;
            return target.CurrentHp() < (Constant + Coefficient * target.GetFinalStatus().MaxHp);
        }
    }
}