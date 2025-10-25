using GfEngine.Battles.Conditions;
using GfEngine.Models.Buffs;
using GfEngine.Core.Conditions;

namespace GfEngine.Battles.Rules
{
    public class ConditionalModifierRule
    {
        public ICondition Condition { get; set; }
        public Modifier ModiferToApply { get; set; }
    }
}