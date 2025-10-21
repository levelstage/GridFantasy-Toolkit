using GfEngine.Battles;
using GfToolkit.Shared;
using System.Collections.Generic;

namespace GfEngine.Behaviors.BehaviorResults
{
    public class AttackResult : BehaviorResult
    {
        public Unit Victim;
        public int Damage;
        public HashSet<BattleTag> Tags;
        public override string ToString()
        {
            return string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_FirstAttack), Agent.Name, Victim.Name, Damage);
        }
    }    
}