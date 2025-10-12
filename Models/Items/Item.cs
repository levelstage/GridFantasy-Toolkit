using System.Collections.Generic;
namespace GfEngine.Models.Items
{
	public abstract class Item
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public List<ItemTag> Itags { get; set; }

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