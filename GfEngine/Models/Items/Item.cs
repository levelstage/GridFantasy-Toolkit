using System.Collections.Generic;
using GfEngine.Models.Buffs;
using GfToolkit.Shared;
namespace GfEngine.Models.Items
{
	public abstract class Item : GameObject
	{
		public string Description { get; set; }
		public HashSet<ItemTag> Itags { get; set; }
		public Buff ItemBuff { get; set; }

		public Item()
		{
			Itags = new HashSet<ItemTag>();
		}
		public Item(Item p)
		{
			Name = p.Name;
			Description = p.Description;
			Itags = new HashSet<ItemTag>(p.Itags);
		}
		public abstract Item Clone();
	}
}