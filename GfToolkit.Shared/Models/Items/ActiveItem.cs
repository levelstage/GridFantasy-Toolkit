using GfToolkit.Shared.Behaviors;
using GfToolkit.Shared.Battles;
using GfToolkit.Shared;

namespace GfToolkit.Shared.Models.Items
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
            Itags = new List<ItemTag>(parent.Itags);
            ItemBehavior = parent.ItemBehavior;
        }

        public override ActiveItem Clone()
        {
            return new ActiveItem(this);
        }
    }
}