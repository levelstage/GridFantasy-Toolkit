namespace GfToolkit.Shared.Dtos.Behaviors
{
    public class SelfInvocationBehaviorDto : BehaviorDto
    {
        public int EffectCode { get; set; } // 자기 자신에게 적용할 버프셋 코드
        public int ChangingHp { get; set; } // 자기 자신에게 적용할 HP 변화량 (양수면 회복, 음수면 피해)
        public SelfInvocationBehaviorDto()
        {
            EffectCode = -1;
            ChangingHp = 0;
            Type = "SelfInvocation";
        }
        public SelfInvocationBehaviorDto(BehaviorDto parent) : base(parent)
        {
            EffectCode = -1;
            ChangingHp = 0;
            Type = "SelfInvocation";
        }
    }
}
