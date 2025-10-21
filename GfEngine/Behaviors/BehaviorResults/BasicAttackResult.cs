using GfEngine.Battles;
using GfToolkit.Shared;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace GfEngine.Behaviors.BehaviorResults
{
    public class BasicAttackResult : AttackResult
    {
        public AttackResult CounterAttackResult { get; set; }
        public bool HadInitiative { get; set; } // 행위자가 선공하는데 성공했는지(false면 반격자 선공)

        public override string ToString()
        {
            string res = ">> ";
            if (CounterAttackResult == null) // 반격이 없다면
            {
                res += base.ToString();
                if (Tags.Contains(BattleTag.killedCounter))
                    res = res + "\n" + string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_DefenderDied), Victim.Name);
                else
                    res = res + "\n" + string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_DefenderCantCounter), Victim.Name);
            }
            else // 반격이 있는 상황
            {
                if (HadInitiative) // 공격자의 선공
                {
                    res += base.ToString();
                    res = res + "\n>> " + string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_ExecuteCounterAttack), Victim.Name, CounterAttackResult.Damage);
                }
                else // 방어자의 선공
                {
                    res += string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_CounterWasFaster), Victim.Name, CounterAttackResult.Damage);
                    res = res + "\n>>" + string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_ExecuteCounterAttack), Agent, Damage);
                } 
            }

            // 전투 후 최종 HP 상태
            res = res + "\n--- " + GameData.Text.Get(GameData.Text.Key.UI_Battle_FinalStateIndicator) + "---";
            res = res + "\n" + string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_AttackerFinalState), Agent.Name, Agent.LiveStat.CurrentHp, Agent.LiveStat.Buffed().MaxHp);
            res = res + "\n" + string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_AttackerFinalState), Victim.Name, Victim.LiveStat.CurrentHp, Victim.LiveStat.Buffed().MaxHp);
            return res;
        }
    }    
}