using System.Collections.Generic;

namespace GfEngine.Battles.Conditions
{
    public class OrCondition : ICondition
    {
        public List<ICondition> Conditions;
        public bool IsMet(BattleContext battleContext)
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