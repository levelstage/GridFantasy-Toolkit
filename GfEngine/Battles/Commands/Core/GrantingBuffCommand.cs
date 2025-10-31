using GfEngine.Battles.Units;
using GfEngine.Models.Buffs;
using GfToolkit.Shared;
using System.Collections.Generic;

namespace GfEngine.Battles.Commands.Core
{
    public class GrantingBuffCommand : Command
    {
        public Unit TargetUnit { get; set; } // 대상.
        public Buff ApplyingBuff { get; set; } // 적용할 버프셋.

        public GrantingBuffCommand() { }
        public GrantingBuffCommand(GrantingBuffCommand parent) : base(parent)
        {
            TargetUnit = parent.TargetUnit;
            ApplyingBuff = new Buff(parent.ApplyingBuff);
        }

        public override bool Execute(BattleContext battleContext)
        {
            return true;
        }
        public override Command Clone()
        {
            return new GrantingBuffCommand(this);
        }

    }
}