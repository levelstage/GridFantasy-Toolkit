using System.Collections.Generic;

namespace GfEngine.Core.Conditions
{
    public class AndCondition : ICondition
    {
        public List<ICondition> Conditions;
        public AndCondition(List<ICondition> conditions)
        {
            Conditions = conditions;
        }

        public bool IsMet(IContext battleContext)
        {
            if (Conditions == null) return false;
            foreach (ICondition condition in Conditions)
            {
                if (!condition.IsMet(battleContext)) return false;
            }
            return true;
        }
    }
}