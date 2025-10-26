using GfEngine.Core.Conditions;
using GfToolkit.Shared;

namespace GfEngine.Models.Buffs.Modifiers
{
    public class ConditionOverrider : Modifier
    {
        public OverridingOperator OverridingBy { get; set; }
        public ICondition ConditionToOverride { get; set; }

        public ConditionOverrider() { }
        public ConditionOverrider(ConditionOverrider parent) : base(parent)
        {
            OverridingBy = parent.OverridingBy;
            ConditionToOverride = parent.ConditionToOverride;
        }
        public override Modifier Clone()
        {
            return new ConditionOverrider(this);
        }
    }
}