using GfEngine.Battles.Patterns;
using GfToolkit.Shared;

namespace GfEngine.Models.Buffs.Modifiers
{
    public class PatternSetOverrider : Modifier
    {
        public OverridingOperator OverridingBy { get; set; }
        public PatternSet PatternSetToOverride { get; set; }
        public PatternSetOverrider() { }
        public PatternSetOverrider(PatternSetOverrider parent) : base(parent)
        {
            OverridingBy = parent.OverridingBy;
            PatternSetToOverride = parent.PatternSetToOverride;
        }
        public override Modifier Clone()
        {
            return new PatternSetOverrider(this);
        }

    }
}