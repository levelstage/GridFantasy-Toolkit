using System.Collections.Generic;
using GfEngine.Battles;

namespace GfEngine.Behaviors.BehaviorResults
{
    public class BundelBehaviorResult : BehaviorResult
    {
        public List<BehaviorResult> BundledBehaviors { get; set; }
        public override string ToString()
        {
            string res = "";
            int finalChecker = 0;
            foreach (BehaviorResult iter in BundledBehaviors)
            {
                if (++finalChecker == BundledBehaviors.Count) res += iter.ToString();
                else res = res + iter.ToString() + "\n";
            }
            return res;
        }
    }    
}