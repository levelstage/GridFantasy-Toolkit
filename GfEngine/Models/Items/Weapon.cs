using System.Collections.Generic;
using GfEngine.Models.Buffs;
using GfToolkit.Shared;
namespace GfEngine.Models.Items
{
	public class Weapon : SymbolItem
	{
		public int Class { get; set; } // 이 무기의 무기군 코드
		public string Fomula { get; set; } // 이 무기의 피해 산출 공식
		public DamageType TypeOfDamage { get; set; } // 이 무기의 피해 유형
		public Buff EquipBuff { get; set; }
		public Weapon()
		{
			Fomula = "0";
			Class = 1;
		}
		public Weapon(Weapon parent)
		{
			
			Code = parent.Code;
			Name = parent.Name;
			Fomula = parent.Fomula;
			Class = parent.Class;
		}
		public override Item Clone()
		{
			return new Weapon(this);
		}
	}
}