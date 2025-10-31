using GfEngine.Battles.Commands;
using GfEngine.Battles.Units;

namespace GfEngine.Battles.Events
{
    // BattleContext를 상속받거나, BattleContext를 필드로 가집니다. (상속 추천)
    public class BattleEventContext : BattleContext
    {
        public Command SourceCommand;

        // BattleContext의 생성자를 그대로 사용하고 추가 데이터를 받습니다.
        public BattleEventContext(
            BattleContext baseContext, 
            Command sourceCommand
        ) : base(baseContext: baseContext)
        {
            SourceCommand = sourceCommand;
        }
    }
}