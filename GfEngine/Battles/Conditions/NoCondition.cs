namespace GfEngine.Battles.Conditions
{
    public class NoCondition : ICondition
    {
        public bool IsMet(BattleContext battleContext)
        {
            return true;
        }
    }
}