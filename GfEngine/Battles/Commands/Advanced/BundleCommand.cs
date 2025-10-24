using System.Collections.Generic;
using GfEngine.Battles;

namespace GfEngine.Battles.Commands.Advanced
{
    public class BundleCommand : Command
    {
        public List<Command> Commands { get; set; }
        public override string ToString()
        {
            string res = "";
            int finalChecker = 0;
            foreach (Command iter in Commands)
            {
                if (++finalChecker == Commands.Count) res += iter.ToString();
                else res = res + iter.ToString() + "\n";
            }
            return res;
        }
        public override void Execute(BattleContext battleContext)
        {
            
        }
    }    
}