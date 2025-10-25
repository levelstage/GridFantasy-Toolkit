using System.Collections.Generic;
using GfEngine.Battles;

namespace GfEngine.Battles.Commands.Advanced
{
    public class BundleCommand : Command
    {
        public List<Command> Commands { get; set; }
        public BundleCommand() { }
        public BundleCommand(BundleCommand parent) : base(parent)
        {
            Commands = new List<Command>(parent.Commands);
        }
        public override void Execute(BattleContext battleContext)
        {
            
        }
        public override Command Clone()
        {
            return new BundleCommand(this);
        }
    }    
}