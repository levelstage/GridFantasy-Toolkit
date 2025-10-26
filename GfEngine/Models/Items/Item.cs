using System.Collections.Generic;
using GfEngine.Models.Buffs;
using GfToolkit.Shared;
namespace GfEngine.Models.Items
{
	public abstract class Item : GameObject
	{
		public string Description { get; set; }
		public HashSet<string> Itags { get; set; }
		public Buff ItemBuff { get; set; }

		public Item()
		{
			Itags = new HashSet<string>();
		}
		public Item(Item p)
		{
			Name = p.Name;
			Description = p.Description;
			Itags = new HashSet<string>(p.Itags);
		}
		public abstract Item Clone();
	}
}