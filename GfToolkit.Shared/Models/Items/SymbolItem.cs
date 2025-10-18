using System.Collections.Generic;
namespace GfToolkit.Shared.Models.Items
{
	public class SymbolItem : Item
	{
		public SymbolItem()
		{

		}
		public SymbolItem(SymbolItem parent)
        {
            Code = parent.Code;
            Name = parent.Name;
            Description = parent.Description;
            Itags = new List<ItemTag>(parent.Itags);
        }
		public override Item Clone()
		{
			return new SymbolItem(this);
        }
	}
}