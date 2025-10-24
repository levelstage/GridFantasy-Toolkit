namespace GfEngine.Battles.Conditions
{
    public interface ICondition
    {
        public bool IsMet(BattleContext battleContext);
    }
}