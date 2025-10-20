using GfEngine.Battles;

namespace GfEngine.Behaviors.BehaviorResults
{
    public abstract class BehaviorResult
    {
        public Unit Agent { get; set; } // 이 Behavior를 시도한 행위자.
        public Square TargetSquare { get; set; } // 행위자가 Behavior를 시도한 칸.
    }    
}