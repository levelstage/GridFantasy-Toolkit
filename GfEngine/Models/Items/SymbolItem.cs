using System.Collections.Generic;
using GfToolkit.Shared;
namespace GfEngine.Models.Items
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
        }
		public override Item Clone()
		{
			return new SymbolItem(this);
        }
	}
}