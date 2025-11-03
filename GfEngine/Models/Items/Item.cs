using System.Collections.Generic;
using GfEngine.Models.Buffs;
using GfToolkit.Shared;
namespace GfEngine.Models.Items
{
	public abstract class Item : GameObject
	{
		public HashSet<string> Itags { get; set; }
		public Buff ItemBuff { get; set; }

		public Item()
		{
			Itags = new HashSet<string>();
		}
		public Item(Item parent) : base(parent)
		{
			Name = parent.Name;
			Description = parent.Description;
			Itags = new HashSet<string>(parent.Itags);
		}
		public abstract Item Clone();
	}
}