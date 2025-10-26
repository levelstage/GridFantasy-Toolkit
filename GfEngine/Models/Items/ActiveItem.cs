using GfEngine.Battles.Behaviors;
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
            Itags = new HashSet<string>(parent.Itags);
            ItemBehavior = parent.ItemBehavior;
        }

        public override ActiveItem Clone()
        {
            return new ActiveItem(this);
        }
    }
}