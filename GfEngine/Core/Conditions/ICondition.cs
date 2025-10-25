namespace GfEngine.Core.Conditions
{
    public interface ICondition
    {
        public bool IsMet(IContext battleContext);
    }
}