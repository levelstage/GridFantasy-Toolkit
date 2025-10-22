namespace GfToolkit.Shared.Dtos.Behaviors
{
    public class AreaInvocationBehaviorDto : BehaviorDto
    {
        public BasicPatternType Area;
        public int DamageConstant;
        public List<(StatType, float)> Coefficients;
        public DamageType DamageType;
        public int ApplyingBuffSetCode;

        public AreaInvocationBehaviorDto()
        {
            Area = BasicPatternType.King;
            DamageConstant = 0;
            Coefficients = new();
            DamageType = DamageType.Physical;
            ApplyingBuffSetCode = -1;
            Type = "AreaInvocation";
        }
        public AreaInvocationBehaviorDto(BehaviorDto parent) : base(parent)
        {
            Area = BasicPatternType.King;
            DamageConstant = 0;
            Coefficients = new();
            DamageType = DamageType.Physical;
            ApplyingBuffSetCode = -1;
            Type = "AreaInvocation";
        }
    }
}
