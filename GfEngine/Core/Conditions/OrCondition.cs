using System.Collections.Generic;

namespace GfEngine.Core.Conditions
{
    public class OrCondition : ICondition
    {
        public List<ICondition> Conditions { get; set; }

        public OrCondition(List<ICondition> conditions)
        {
            Conditions = conditions;
        }
        public bool IsMet(IContext battleContext)
        {
            if (Conditions == null) return false;
            foreach (ICondition condition in Conditions)
            {
                if (condition.IsMet(battleContext)) return true;
            }
            return false;
        }
    }
}