using System.Collections.Generic;
using GfEngine.Battles.Units;
using GfEngine.Core.Conditions;
using GfEngine.Models.Buffs;
using GfToolkit.Shared;

namespace GfEngine.Battles.Squares
{
    public class GroundEffect : GameObject
    {   
        // 이 효과를 누가 만들었는가?
        public Unit Source { get; set; } 
        
        // 이 효과가 적용될 대상은 누구인가? (적, 아군, 중립 등)
        public ICondition EffectCondition { get; set; }
        
        // 대상에게 실제로 적용될 버프/디버프 효과
        public Buff ApplyingBuff { get; set; }
        
        // 이 효과가 몇 턴 동안 지속되는가?
        public int Duration { get; set; }

        public GroundEffect()
        {

        }
        public GroundEffect(GroundEffect parent) : base(parent)
        {
            Source = parent.Source;
            EffectCondition = parent.EffectCondition;
            ApplyingBuff = new Buff(parent.ApplyingBuff);
            Duration = parent.Duration;
        }
    }
}