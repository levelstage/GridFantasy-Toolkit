using GfEngine.Battles.Squares;
using GfEngine.Battles.Units;
using GfEngine.Models.Buffs;
using GfToolkit.Shared;
using System.Collections.Generic;

namespace GfEngine.Battles.Commands.Core
{
    public class ApplyGroundEffectCommand : Command
    {
        public GroundEffect ApplyingGroundEffect { get; set; } // 적용할 GroundEffect.
        public ApplyGroundEffectCommand() { }
        public ApplyGroundEffectCommand(ApplyGroundEffectCommand parent) : base(parent)
        {
            ApplyingGroundEffect = new GroundEffect(parent.ApplyingGroundEffect);
        }

        public override bool Execute(BattleContext battleContext)
        {
            return true;
        }
        public override Command Clone()
        {
            return new ApplyGroundEffectCommand(this);
        }

    }
}