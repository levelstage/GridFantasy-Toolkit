using GfToolkit.Shared.Battles;
using System.Collections.Generic;
namespace GfToolkit.Shared.Models.Buffs
{
	public class BuffSet // 진정한 객체로서의 버프. 여러 Buff들의 묶음임.
	{
		public BuffSetCode Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Duration { get; set; } // 이 '효과 묶음'의 지속시간
		// 부차적인 속성
		public Unit Source { get; set; }  // 버프를 준 Source Unit. 유닛에게서 받은 버프가 아니라면 null
		// 아래 두 항목은 둘 다 참일수는 없지만 둘 다 거짓일 수는 있다. 스킬이나 특성으로 얻은 Buff 등.
		public bool IsBuff { get; set; } // 게임 상에서 "버프"로 분류되는지? 참이면 버프 해제 스킬 등에 영향을 받음.
		public bool IsDebuff { get; set; } // 마찬가지로 게임 상에서 "디버프"로 분류되는지? 만약 참이라면, 정화 스킬 등에 해제됨.
		// 통상 버프/디버프이더라도 해제 불가 기능이 있을 수 있으므로,
		public bool Removable { get; set; } // 해제 가능한지. 
		public bool Visible { get; set; } // 눈에 보이는 버프인지. 스킬이나 특성 등은 보이지 않음!


		// 이 BuffSet이 가진 실제 효과들의 목록
		public List<Buff> Effects { get; set; }

		public BuffSet()
		{

		}

		public BuffSet(BuffSet p)
		{
			Source = null;
			IsBuff = p.IsBuff;
			IsDebuff = p.IsDebuff;
			Removable = p.Removable;
		}
	}
}