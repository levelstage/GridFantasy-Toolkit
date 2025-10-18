using System.Collections.Generic;
using GfToolkit.Shared.Models.Buffs;
namespace GfToolkit.Shared.Models.Items
{
	public abstract class Item
	{
		public int Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<ItemTag> Itags { get; set; }
		public BuffSet ItemBuff { get; set; }

		public Item()
		{
			Itags = new List<ItemTag>();
		}
		public Item(Item p)
		{
			Name = p.Name;
			Description = p.Description;
			Itags = new List<ItemTag>(p.Itags);
		}
		public abstract Item Clone();
	}
}