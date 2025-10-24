namespace GfToolkit.Shared.Dtos.Models
{
    public class ActorDto : GameDto
    {
		public int[] Stat = new int[6];
		public int[] BaseGrowthRates = new int[6];
		public MoveType MoveClass { get; set; }
		public WeaponType WeaponClass { get; set; }
		public int UniqueSkillCode { get; set; }
		public int EquipmentCode { get; set; }
		public List<int> InventoryByCode { get; set; }
		public List<int> TraitCodes { get; set; }

		// 레벨업에 따른 고정 특성, 스킬 강화 특성.
		public Dictionary<int, (int, int)> FixedTraitCodes { get; set; }
		public Dictionary<int, (int, int)> UniqueSkillTraitCodes { get; set; }
		public List<int> ForbiddenTraitCodes { get; set; } // 이 캐릭터에게 금지된 특성들. (고유스킬과의 과한 시너지, 충돌 등으로 인해)

    }
}