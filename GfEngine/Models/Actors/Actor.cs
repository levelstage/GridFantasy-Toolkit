using GfEngine.Models.Classes;
using GfEngine.Models.Items;
using GfEngine.Models.Statuses;
using GfToolkit.Shared;
using System.Collections.Generic;

namespace GfEngine.Models.Actors
{
	public class Actor : GameObject
	{
		public Status Stat;
		public GrowthRates BaseGrowthRates;
		public int PresentCrest { get; set; } // 적용된 문장의 코드
		public List<int> UsableWeapons { get; set; } // 사용 가능한 무기군의 코드들
		public Skill UniqueSkill { get; set; }
		public Weapon Equipment { get; set; }
		public List<Item> Inventory { get; set; }
		public List<Trait> Traits { get; set; }

		// 레벨업에 따른 고정 특성, 스킬 강화 특성.
		public Dictionary<int, List<Trait>> FixedTraits { get; set; }
		public Dictionary<int, List<Trait>> UniqueSkillTraits { get; set; }
		public List<Trait> ForbiddenTraits { get; set; } // 이 캐릭터에게 금지된 특성들. (고유스킬과의 과한 시너지, 충돌 등으로 인해)

		public Actor() { } // 기본 생성자는 사용하지 않지만, { } 생성을 위해 만들어둠.
		public Actor(Actor p)
		{
			Stat = p.Stat;
			PresentCrest = p.PresentCrest;
			UsableWeapons = p.UsableWeapons;
			Name = p.Name;
			UniqueSkill = p.UniqueSkill;
			if (p.Equipment != null) Equipment = new Weapon(p.Equipment);
			Inventory = new List<Item>(p.Inventory);
			foreach (Item i in p.Inventory) Inventory.Add(i.Clone()); // 아이템 각각을 새로운 객체로 복사.
			Traits = new List<Trait>(p.Traits); // 특성은 어떤 특성이 있는지만 알면 되므로 각각을 복사할 필요 x.
			//FixedTraits = new Dictionary<int, List<Trait>>(p.FixedTraits);
			//UniqueSkillTraits = new Dictionary<int, List<Trait>>(p.UniqueSkillTraits);
		}
	}
}
