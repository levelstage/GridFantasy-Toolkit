using GfEngine.Core.Conditions;
using GfEngine.Battles.Patterns;

namespace GfEngine.Battles.Rules
{
    public class ConditionalPatternRule
    {
        public ICondition Condition { get; set; }
        public PatternSet PatternSetToUse { get; set; }
    }
}