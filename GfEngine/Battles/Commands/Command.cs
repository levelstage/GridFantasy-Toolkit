using GfEngine.Battles.Units;
using GfEngine.Battles.Squares;
using GfEngine.Battles.Conditions;

namespace GfEngine.Battles.Commands
{
    public enum CommandPriority
    {
        // 1. [가장 높음] 즉시 실행/인과관계 유지 (Post-Damage Immediate)
        // 피해를 준 유닛의 직접적인 결과 (생명력 흡수, 피해량 증폭/감소 등)
        ImmediateEffect = 3,

        // 2. [중간] 일반 반응 (Reaction)
        // 피해를 받은 유닛의 방어/반격 행동 (반격, 배리어 발동, 특정 버프 해제)
        ReactionBehavior = 2,

        // 3. [낮음] Cleanup 및 시스템 명령
        // 상태 변화 확인, 다음 턴 준비, 사망 처리 등
        CleanupAndSystem = 1,

        // 4. [기본] 일반적인 턴 명령 (BasicAttackCommand, MoveCommand 등)
        // 모든 커맨드는 기본적으로 StandardAction이고, 위에 있는 것들은 이벤트가 부여함. 
        StandardAction = 0
    }
    public abstract class Command
    {
        public CommandPriority ExecutionPriority { get; set; } = CommandPriority.StandardAction;
        public Unit SourceUnit { get; set; } // 이 Behavior를 시도한 행위자.
        public Square TargetSquare { get; set; } // 행위자가 Behavior를 시도한 칸.
        public Command() { }
        public Command(Command parent)
        {
            SourceUnit = parent.SourceUnit;
            TargetSquare = parent.TargetSquare;
        }
        public abstract bool Execute(BattleContext battleContext);
        public abstract Command Clone();
    }    
}