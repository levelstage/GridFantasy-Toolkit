using GfEngine.Battles.Patterns;
using System.Collections.Generic;
using GfToolkit.Shared;
namespace GfEngine.Models.Buffs.Modifiers
{
	public class Aura : Modifier
	{
		// 오라형 버프 효과
		public PatternSet AuraScope { get; set; } // 오라의 범위 (PatternSet 사용)
		public int AuraEffect { get; set; } // 오라 범위 내의 대상에게 부여할 버프셋
		public HashSet<Relation> AuraTargets { get; set; } // 오라의 대상 (아군, 적군 등)
		// 동적 범위 지정 관련 속성들
		public bool UseMovePattern { get; set; } = false; // 유닛의 현재 이동 범위를 사용할지 여부
		public bool UseAttackPattern { get; set; } = false; // 유닛의 현재 공격 범위를 사용할지 여부


		public Aura()
		{

		}

		public Aura(Aura p) : base(p)
		{
			this.AuraScope = p.AuraScope;
			this.AuraEffect = p.AuraEffect;
			this.AuraTargets = p.AuraTargets;
			this.UseMovePattern = p.UseMovePattern;
			this.UseAttackPattern = p.UseAttackPattern;
		}
	}
}