using GfEngine.Battles.Units;

namespace GfEngine.Battles.Events
{
    // BattleContext를 상속받거나, BattleContext를 필드로 가집니다. (상속 추천)
    public class BattleEventContext : BattleContext 
    {
        // 1. 이벤트의 주 데이터 (예: 최종 피해량, 회복량)
        public float PrimaryValue { get; set; }
        
        // 2. 이벤트의 부가 데이터 (예: 버프 코드)
        public object SecondaryData { get; set; } 
        
        // 3. 이벤트의 출처가 된 Command나 Unit (옵션)
        public Unit EventSourceUnit { get; set; }

        // BattleContext의 생성자를 그대로 사용하고 추가 데이터를 받습니다.
        public BattleEventContext(
            BattleContext baseContext, 
            float primaryValue, 
            object secondaryData = null
        ) : base(
            waveData: baseContext.WaveData,
            originSquare: baseContext.OriginSquare,
            targetSquare: baseContext.TargetSquare,
            explicitOriginUnit: baseContext.OriginUnit,
            explicitTargetUnit: baseContext.TargetUnit
        )
        {
            PrimaryValue = primaryValue;
            SecondaryData = secondaryData;
        }
    }
}