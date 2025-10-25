namespace GfEngine.Core.Conditions
{
    public class NotCondition : ICondition
    {
        public ICondition Condition;
        public bool IsMet(IContext battleContext)
        {
            if (Condition == null) return false;
            return !Condition.IsMet(battleContext);
        }
    }
}