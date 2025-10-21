using GfEngine.Behaviors;
using GfEngine.Battles;
using GfToolkit.Shared;
using System.Collections.Generic;

namespace GfEngine.Models.Items
{
    public class ActiveItem : Item
    {
        public Behavior ItemBehavior;
        public ActiveItem()
        {

        }
        public ActiveItem(ActiveItem parent)
        {
            Code = parent.Code;
            Name = parent.Name;
            Description = parent.Description;
            Itags = new HashSet<ItemTag>(parent.Itags);
            ItemBehavior = parent.ItemBehavior;
        }

        public override ActiveItem Clone()
        {
            return new ActiveItem(this);
        }
    }
}