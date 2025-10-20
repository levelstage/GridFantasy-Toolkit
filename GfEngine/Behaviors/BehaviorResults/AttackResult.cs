using GfEngine.Battles;
using GfToolkit.Shared;
using System.Collections.Generic;

namespace GfEngine.Behaviors.BehaviorResults
{
    public class AttackResult : BehaviorResult
    {
        public Unit Victim;
        public int Damage;
        public List<BattleTag> Tags;
    }    
}