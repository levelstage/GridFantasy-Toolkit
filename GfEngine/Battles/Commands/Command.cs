using GfEngine.Battles.Units;
using GfEngine.Battles.Squares;
using GfEngine.Battles.Conditions;

namespace GfEngine.Battles.Commands
{
    public abstract class Command
    {
        public Unit Agent { get; set; } // 이 Behavior를 시도한 행위자.
        public Square TargetSquare { get; set; } // 행위자가 Behavior를 시도한 칸.
        public Command() { }
        public Command(Command parent)
        {
            Agent = parent.Agent;
            TargetSquare = parent.TargetSquare;
        }
        public abstract void Execute(BattleContext battleContext);
    }    
}