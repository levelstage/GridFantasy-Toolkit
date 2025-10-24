namespace GfEngine.Battles.Conditions
{
    public class NotCondition : ICondition
    {
        public ICondition Condition;
        public bool IsMet(BattleContext battleContext)
        {
            if (Condition == null) return false;
            return !Condition.IsMet(battleContext);
        }
    }
}