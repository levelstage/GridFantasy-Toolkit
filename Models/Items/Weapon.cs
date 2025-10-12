using System.Collections.Generic;
namespace GfEngine.Models.Items
{
	public class Weapon : Item
	{
		public WeaponCode Code { get; set; }
		public int Power { get; set; }
		public WeaponType Type { get; set; }
		public List<WeaponTag> WTags { get; set; }
		public Weapon()
		{
			Power = 10;
			Type = WeaponType.Spear;
			WTags = new List<WeaponTag>();
		}
		public Weapon(Weapon parent)
		{
			Code = parent.Code;
			Name = parent.Name;
			Power = parent.Power;
			Type = parent.Type;
			WTags = new List<WeaponTag>(parent.WTags);
		}
		public override Item Clone()
		{
			return new Weapon(this);
		}
	}
}