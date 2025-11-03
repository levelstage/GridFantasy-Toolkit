using GfEngine.Models.Items;
using GfEngine.Battles.Patterns;
using GfEngine.Battles.Units;
using GfEngine.Battles.Squares;
using GfEngine.Battles.Commands;
using GfEngine.Battles.Commands.Advanced;
using GfEngine.Battles.Commands.Core;
using System.Collections.Generic;
using System;
using GfToolkit.Shared;
namespace GfEngine.Battles.Behaviors.Complexed
{
    public class BasicAttackBehavior : Behavior
    {
        public override Command Execute(BattleContext context)
        {
            Unit o = context.OriginUnit;
			Square ts = context.TargetSquare;
            (string fomula, DamageType damageType) = o.GetBasicAttackInfo();
			BasicAttackCommand res = new BasicAttackCommand()
            {
                SourceUnit = o,
                TargetSquare = ts,
                AttackCommand = new HitCommand()
                {
                    SourceUnit = o,
                    TargetSquare = ts,
                    TargetUnit = ts.Occupant,
                    Fomula = fomula,
                    Type = damageType
                }
            };
			return res;
        }
    }
}
