using System;
using GfEngine.Battles;

namespace GfEngine.Behaviors.BehaviorResults
{
    public class MoveResult : BehaviorResult
    {
        public string MovedSquareDescription { get; set; } // 판 크기에 따라 같은 좌표여도 칸의 이름이 바뀔 수 있으므로.
        public override string ToString()
        {
            return string.Format(GameData.Text.Get(GameData.Text.Key.UI_Behavior_CharacterMoved), Agent.Name, MovedSquareDescription);
        }
    }    
}