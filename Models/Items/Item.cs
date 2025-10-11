using System.Collections.Generic;
namespace GfEngine.Models.Items
{
	public class Item
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public List<ItemTag> iTags { get; set; }
		public Item()
		{
			this.iTags = new List<ItemTag>();
		}
		public Item(Item p)
		{

		}
	}
}