using GfEngine.Models.Buffs;
using GfEngine.Battles;
using GfToolkit.Shared;
using System.Collections.Generic;
using GfEngine.Battles.Units;
using GfEngine.Models.Actors;
using GfEngine.Models.Items;
namespace GfEngine.Models.Statuses
{
	public class LiveStatus
	{
		public Status Stat { get; }
		public int CurrentHp { get; set; }
		public int PresentCrest { get; set; } // 적용된 문장의 코드
		public List<int> UsableWeapons { get; set; } // 사용 가능한 무기군의 코드들
		public Skill UniqueSkill { get; set; }
		public Weapon Equipment { get; set; }
		public List<Item> Inventory { get; set; }
		public List<Trait> Traits { get; set; }
		public List<Buff> Buffs { get; set; }
		public readonly List<Modifier> EffectiveModifiers = new List<Modifier>();

		// 생성자: '기본 스탯(Status)'을 바탕으로 '현재 상태'를 생성.
		public LiveStatus(Status baseStat)
		{
			Stat = baseStat;
			// 현재 체력은 최대 체력과 같게 초기화.
			CurrentHp = baseStat.MaxHp;
			// 빈 버프 리스트 생성.
			Buffs = new List<Buff>();
		}
	}
}