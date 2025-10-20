using GfEngine.Battles;
using System.Collections.Generic;
namespace GfEngine.Behaviors
{
    public class BundleBehavior() : Behavior
    {
        public List<Behavior> Behaviors { get; set; }
        public override string Execute (Square origin, Square target, Square[,] map)
        {
            string result = "@B";
            foreach(Behavior iter in Behaviors)
            {
                result = result + "$" + iter.Execute(origin, target, map);
                // 만약 unit이 이동했다면, origin
            }
            return result;
        }
}
}