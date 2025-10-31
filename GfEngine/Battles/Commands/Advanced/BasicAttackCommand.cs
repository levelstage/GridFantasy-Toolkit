using GfEngine.Battles.Commands.Core;
using GfToolkit.Shared;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace GfEngine.Battles.Commands.Advanced
{
    public class BasicAttackCommand : BundleCommand
    {
        public HitCommand AttackCommand { get; set; }
        public HitCommand CounterAttackCommand { get; set; }
        public bool HadInitiative { get; set; } // 행위자가 선공하는데 성공했는지(false면 반격자 선공)
        public BasicAttackCommand() { }
        public BasicAttackCommand(BasicAttackCommand parent) : base(parent)
        {
            AttackCommand = parent.AttackCommand.Clone() as HitCommand;
            CounterAttackCommand = parent.CounterAttackCommand.Clone() as HitCommand;
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