using GfEngine.Battles.Units;
using GfEngine.Battles.Squares;
using GfEngine.Battles.Conditions;

namespace GfEngine.Battles.Commands
{
    public class NullCommand : Command
    {
        public NullCommand() { }
        public override bool Execute(BattleContext battleContext)
        {
            // NullCommand가 발생하면 아무 행동도 하지 않고, 행동은 실패로 간주한다.
            return false;
        }
        public override Command Clone()
        {
            return new NullCommand();
        }
    }    
}