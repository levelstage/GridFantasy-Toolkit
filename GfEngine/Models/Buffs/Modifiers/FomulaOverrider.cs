using GfToolkit.Shared;

namespace GfEngine.Models.Buffs.Modifiers
{
    public class FomulaOverrider : Modifier
    {
        public OverridingOperator OverridingBy { get; set; }
        public string FomulaToOverride { get; set; }

        public FomulaOverrider() { }
        public FomulaOverrider(FomulaOverrider parent) : base(parent)
        {
            OverridingBy = parent.OverridingBy;
            FomulaToOverride = parent.FomulaToOverride;
        }
        public override Modifier Clone()
        {
            return new FomulaOverrider(this);
        }
    }
}