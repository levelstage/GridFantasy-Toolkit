namespace GfToolkit.Shared.Dtos.Behaviors
{
    public class AreaEffectBehaviorDto : BehaviorDto
    {
        public BasicPatternType Area;
        public int DamageConstant;
        public List<(StatType, float)> Coefficients;
        public DamageType DamageType;
        public int ApplyingBuffSetCode;

        public AreaEffectBehaviorDto()
        {
            Area = BasicPatternType.King;
            DamageConstant = 0;
            Coefficients = new();
            DamageType = DamageType.Physical;
            ApplyingBuffSetCode = -1;
            Type = "AreaEffect";
        }
        public AreaEffectBehaviorDto(BehaviorDto parent) : base(parent)
        {
            Area = BasicPatternType.King;
            DamageConstant = 0;
            Coefficients = new();
            DamageType = DamageType.Physical;
            ApplyingBuffSetCode = -1;
            Type = "AreaEffect";
        }
    }
}
