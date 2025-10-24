using GfEngine.Battles.Conditions;
using GfEngine.Models.Buffs;

namespace GfEngine.Battles.Rules
{
    public class ConditionalModifierRule
    {
        public ICondition Condition { get; set; }
        public Modifier ModiferToApply { get; set; }
    }
}