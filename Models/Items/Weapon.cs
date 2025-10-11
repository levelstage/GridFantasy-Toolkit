namespace GfEngine.Models.Items
{
	public class Weapon : Item
	{
		public int Power { get; set; }
		public WeaponType Type { get; set; }
		public List<WeaponTag> wTags { get; set; }
		public Weapon()
		{
			this.Power = 10;
			this.Type = WeaponType.Spear;
			this.wTags = new List<WeaponTag>();
		}
		public Weapon(Weapon parent)
		{
			this.Name = parent.Name;
			this.iTags = new List<ItemTag>(parent.iTags);
			this.Power = parent.Power;
			this.Type = parent.Type;
			this.wTags = new List<WeaponTag>(parent.wTags);
		}
	}
}