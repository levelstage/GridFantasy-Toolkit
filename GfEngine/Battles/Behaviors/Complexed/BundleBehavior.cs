using GfEngine.Battles.Squares;
using GfEngine.Battles.Commands;
using System.Collections.Generic;
using GfEngine.Battles.Commands.Advanced;
namespace GfEngine.Battles.Behaviors.Complexed
{
    public class BundleBehavior() : Behavior
    {
        public List<Behavior> Behaviors { get; set; }
        public override Command Execute (Square origin, Square target, Square[,] map)
        {
            BundleCommand command = new BundleCommand() {
                TargetSquare = target,
                Commands = new List<Command>()
            };
            if (origin.Occupant == null) return command;
            command.Agent = origin.Occupant;
            foreach(Behavior iter in Behaviors)
            {
                command.Commands.Add(iter.Execute(origin, target, map));
                
            }
            return command;
        }
}
}