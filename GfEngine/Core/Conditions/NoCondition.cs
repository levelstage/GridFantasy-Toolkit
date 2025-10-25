namespace GfEngine.Core.Conditions
{
    public class NoCondition : ICondition
    {
        public bool IsMet(IContext battleContext)
        {
            return true;
        }
    }
}