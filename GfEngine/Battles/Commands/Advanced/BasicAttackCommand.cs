using GfEngine.Battles.Commands.Core;
using GfToolkit.Shared;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace GfEngine.Battles.Commands.Advanced
{
    public class BasicAttackCommand : Command
    {
        public HitCommand AttackCommand { get; set; }
        public BasicAttackCommand() { }
        public BasicAttackCommand(BasicAttackCommand parent) : base(parent)
        {
            AttackCommand = parent.AttackCommand.Clone() as HitCommand;
        }
        public override bool Execute(BattleContext battleContext)
        {
            return true;
        }
        public override Command Clone()
        {
            return new BasicAttackCommand(this);
        }
    }
}