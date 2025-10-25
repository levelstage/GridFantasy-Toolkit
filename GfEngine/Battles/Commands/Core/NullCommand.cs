using GfEngine.Battles.Units;
using GfEngine.Battles.Squares;
using GfEngine.Battles.Conditions;

namespace GfEngine.Battles.Commands
{
    public class NullCommand : Command
    {
        public NullCommand() { }
        public override void Execute(BattleContext battleContext)
        {
            // 아무 행동도 하지 않는다.
        }
        public override Command Clone()
        {
            return new NullCommand();
        }
    }    
}