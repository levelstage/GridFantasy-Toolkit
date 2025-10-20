using GfEngine.Battles;
using GfToolkit.Shared;
using System.Collections.Generic;

namespace GfEngine.Behaviors.BehaviorResults
{
    public class BasicAttackResult : AttackResult
    {
        public AttackResult CounterAttackResult { get; set; }
        public bool HadInitiative { get; set; } // 행위자가 선공하는데 성공했는지(false면 반격자 선공.)
    }    
}