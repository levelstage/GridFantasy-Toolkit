using System.Collections.Generic;
using GfEngine.Models.Buffs;
using GfToolkit.Shared;
namespace GfEngine.Models.Items
{
	public class Weapon : Item
	{
		public float Power { get; set; } // 공격시 공격력에 곱해질 계수
		public WeaponType Type { get; set; }
		public Buff EquipBuff { get; set; }
		public Weapon()
		{
			Power = 10;
			Type = WeaponType.Spear;
		}
		public Weapon(Weapon parent)
		{
			
			Code = parent.Code;
			Name = parent.Name;
			Power = parent.Power;
			Type = parent.Type;
		}
		public override Item Clone()
		{
			return new Weapon(this);
		}
	}
}