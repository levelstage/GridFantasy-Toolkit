using GfEngine.Battles.Rules;
using GfEngine.Battles.Units;
using System.Collections.Generic;
namespace GfEngine.Models.Buffs
{
	public class Buff : GameObject // 진정한 객체로서의 버프. 여러 Modifier들의 묶음임.
	{
		public string Description { get; set; }
		public int Duration { get; set; } // 이 '효과 묶음'의 지속시간
		// 부차적인 속성
		public Unit Source { get; set; }  // 버프를 준 Source Unit. 유닛에게서 받은 버프가 아니라면 null
		// 아래 두 항목은 둘 다 참일수는 없지만 둘 다 거짓일 수는 있다. 스킬이나 특성으로 얻은 버프 등.
		public bool IsBuff { get; set; } // 게임 상에서 "버프"로 분류되는지? 참이면 버프 해제 스킬 등에 영향을 받음.
		public bool IsDebuff { get; set; } // 마찬가지로 게임 상에서 "디버프"로 분류되는지? 만약 참이라면, 정화 스킬 등에 해제됨.
		public bool Removable { get; set; } // 통상 버프/디버프이더라도 해제 불가 기능이 있을 수 있으므로, 해제 가능한지. 
		public bool Visible { get; set; } // 눈에 보이는 버프인지. 내부적 처리를 위해 존재하는 버프들은 숨긴다.


		// 이 Buff가 가진 실제 효과들의 목록
		public List<ConditionalModifierRule> Modifiers { get; set; }

		public Buff()
		{

		}

		public Buff(Buff parent) : base(parent)
		{
			Description = parent.Description;
			Duration = parent.Duration;
			Source = null;
			IsBuff = parent.IsBuff;
			IsDebuff = parent.IsDebuff;
			Removable = parent.Removable;
			Visible = parent.Visible;
			Modifiers = new List<ConditionalModifierRule>();
			foreach (ConditionalModifierRule cmr in parent.Modifiers)
			{
				Modifiers.Add(new ConditionalModifierRule(cmr));
			}
		}
	}
}