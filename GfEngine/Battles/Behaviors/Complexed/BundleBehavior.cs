using GfEngine.Battles.Squares;
using GfEngine.Battles.Commands;
using System.Collections.Generic;
using GfEngine.Battles.Commands.Advanced;
using GfEngine.Core;
using GfEngine.Battles.Units;
namespace GfEngine.Battles.Behaviors.Complexed
{
    public class BundleBehavior() : Behavior
    {
        public List<Behavior> Behaviors { get; set; }
        public override Command Execute (BattleContext context)
        {
            Square ts = context.TargetSquare;
            Unit o = context.OriginUnit;
            if (o == null) return new NullCommand(); // 시전자가 null이면, 아무것도 안함.
            BundleCommand command = new BundleCommand()
            {
                TargetSquare = ts,
                Commands = new List<Command>()
            };
            command.Agent = o;
            foreach (Behavior iter in Behaviors)
            {
                command.Commands.Add(iter.Execute(context));
            }
            return command;
        }
}
}