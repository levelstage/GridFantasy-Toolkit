using GfEngine.Models.Buffs;
using GfEngine.Battles;
using GfToolkit.Shared;
using System.Collections.Generic;
namespace GfEngine.Behaviors
{
    // 자기 자신에게만 효과를 발동시키는 모든 Behavior
    public class SelfEffectBehavior : Behavior
    {
        public BuffSet Effect { get; set; } // 자기 자신에게 적용할 버프셋
        public int ChangingHp { get; set; } // 자기 자신에게 적용할 HP 변화량 (양수면 회복, 음수면 피해)
        public SelfEffectBehavior()
        {
            Name = "Self Effect";
            Description = "Apply a buff/debuff to self.";
            Scope = new PatternSet(new List<Pattern> { new Pattern { X = 0, Y = 0 } }); // 자기 자신 위치가 범위.
            Accessible = new HashSet<TeamType> { TeamType.Same }; // 자기 자신.
            ApCost = 1; // 기본 행동력 소모량
            Effect = new BuffSet();
        }
        public override string Execute(Square origin, Square target, Square[,] map)
        {
            if (origin.Occupant != null)
            {
                origin.Occupant.LiveStat.Buffs.Add(new BuffSet(Effect) { Source = origin.Occupant });
                origin.Occupant.TakeDamage(-ChangingHp, DamageType.True); // 음수 피해량은 회복, 저항력 계산 회피.
                return $"{origin.Occupant.Name} applies {Effect.Name} to self.";
            }
            return "No occupant to apply effect.";
        }
    }
}