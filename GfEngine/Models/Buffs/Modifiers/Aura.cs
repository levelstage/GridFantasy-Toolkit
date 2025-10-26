using GfEngine.Battles.Patterns;
using System.Collections.Generic;
using GfToolkit.Shared;
using GfEngine.Core.Conditions;
namespace GfEngine.Models.Buffs.Modifiers
{
	public class Aura : Modifier
	{
		// 오라형 버프 효과
		public PatternSet AuraScope { get; set; } // 오라의 범위 (PatternSet 사용)
		public Buff AuraEffect { get; set; } // 오라 범위 내의 대상에게 부여할 버프셋
		public ICondition AuraCondition { get; set; } // 오라를 적용할 조건

		// 동적 범위 지정 관련 속성들
		public bool UseMovePattern { get; set; } = false; // 유닛의 현재 이동 범위를 사용할지 여부
		public bool UseAttackPattern { get; set; } = false; // 유닛의 현재 공격 범위를 사용할지 여부


		public Aura()
		{

		}

		public Aura(Aura p) : base(p)
		{
			AuraScope = new PatternSet(p.AuraScope);
			AuraEffect = new Buff(p.AuraEffect);
			AuraCondition = p.AuraCondition;
			UseMovePattern = p.UseMovePattern;
			UseAttackPattern = p.UseAttackPattern;
		}
        public override Modifier Clone()
        {
			return new Aura(this);
        }
	}
}