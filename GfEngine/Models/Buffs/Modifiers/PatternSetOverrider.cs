using GfEngine.Battles.Patterns;
using GfToolkit.Shared;

namespace GfEngine.Models.Buffs.Modifiers
{
    public class PatternSetOverrider : Modifier
    {
        public OverridingOperator OverridingBy { get; set; }
        public PatternSet PatternSetToOverride { get; set; }
    }
}