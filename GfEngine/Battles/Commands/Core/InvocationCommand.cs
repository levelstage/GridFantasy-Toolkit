using GfEngine.Battles.Units;
using GfEngine.Models.Buffs;
using GfToolkit.Shared;
using System.Collections.Generic;

namespace GfEngine.Battles.Commands.Core
{
    public class InvocationCommand : Command
    {
        public Unit TargetUnit { get; set; } // 대상.
        public Buff ApplyingBuff { get; set; } // 적용할 버프셋.

        public override void Execute(BattleContext battleContext)
        {

        }
    }
}