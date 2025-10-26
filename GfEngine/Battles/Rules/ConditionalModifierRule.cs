using GfEngine.Models.Buffs;
using GfEngine.Core.Conditions;

namespace GfEngine.Battles.Rules
{
    public class ConditionalModifierRule
    {
        public ICondition Condition { get; set; }
        public Modifier ModiferToApply { get; set; }
        public ConditionalModifierRule() { }
        public ConditionalModifierRule(ConditionalModifierRule parent)
        {
            Condition = parent.Condition;
            ModiferToApply = parent.ModiferToApply.Clone();
        }
    }
}