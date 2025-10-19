namespace GfToolkit.Shared.Dtos.Behaviors
{
    public class AreaAttackBehaviorDto : BehaviorDto
    {
        public BasicPatternType Area;
        public int DamageConstant;
        public List<(StatType, float)> Coefficients;
        public DamageType DamageType;
        public int ApplyingBuffSetCode;

        public AreaAttackBehaviorDto()
        {
            Area = BasicPatternType.King;
            DamageConstant = 0;
            Coefficients = new();
            DamageType = DamageType.Physical;
            ApplyingBuffSetCode = -1;
            Type = "AreaAttack";
        }
    }
}
