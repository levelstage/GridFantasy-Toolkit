using GfEngine.Core.Conditions;
using GfToolkit.Shared;

namespace GfEngine.Models.Buffs.Modifiers
{
    public class ConditionOverrider : Modifier
    {
        public OverridingOperator OverridingBy { get; set; }
        public ICondition ConditonToOverride { get; set; }
    }
}