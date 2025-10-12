using GfEngine.Models.Items;
using GfEngine.Models.Statuses;
using System.Collections.Generic;

namespace GfEngine.Models.Actors
{
	public class Actor
	{
		public Status Stat;
		public GrowthRates BaseGrowthRates;
		public MoveType MoveClass { get; set; }
		public WeaponType WeaponClass { get; set; }
		public string Name { get; set; }
		public Skill UniqueSkill { get; set; }
		public Weapon Equipment { get; set; }
		public List<Item> Inventory { get; set; }
		public List<Trait> Traits { get; set; }

		// 레벨업에 따른 고정 특성, 스킬 강화 특성.
		public Dictionary<int, List<Trait>> FixedTraits { get; set; }
		public Dictionary<int, List<Trait>> UniqueSkillTraits { get; set; }
		// 이 캐릭터에게 금지된 특성들. (고유스킬과의 과한 시너지, 충돌 등으로 인해)
		public List<Trait> ForbiddenTraits { get; set; }

		public Actor() { } // 기본 생성자는 사용하지 않지만, { } 생성을 위해 만들어둠.
		public Actor(Actor p)
		{
			Stat = p.Stat;
			MoveClass = p.MoveClass;
			WeaponClass = p.WeaponClass;
			Name = p.Name;
			UniqueSkill = p.UniqueSkill;
			if (p.Equipment != null) Equipment = new Weapon(p.Equipment);
			Inventory = new List<Item>(p.Inventory);
			foreach (Item i in p.Inventory) Inventory.Add(i.Clone()); // 아이템 각각을 새로운 객체로 복사.
			Traits = new List<Trait>(p.Traits); // 특성은 어떤 특성이 있는지만 알면 되므로 각각을 복사할 필요 x.
			FixedTraits = new Dictionary<int, List<Trait>>(p.FixedTraits);
			UniqueSkillTraits = new Dictionary<int, List<Trait>>(p.UniqueSkillTraits);
		}
	}
}
