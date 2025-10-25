using System;
using GfEngine.Battles;
using GfEngine.Battles.Conditions;

namespace GfEngine.Battles.Commands.Core
{
    public class MoveCommand : Command
    {
        public string MovedSquareDescription { get; set; } // 판 크기에 따라 같은 좌표여도 칸의 이름이 바뀔 수 있으므로.

        public MoveCommand() { }
        public MoveCommand(MoveCommand parent) : base(parent)
        {
            MovedSquareDescription = parent.MovedSquareDescription;
        }

        public override string ToString()
        {
            return string.Format(GameData.Text.Get(GameData.Text.Key.UI_Behavior_CharacterMoved), SourceUnit.Name, MovedSquareDescription);
        }
        public override Command Clone()
        {
            return new MoveCommand(this);
        }
        public override void Execute(BattleContext battleContext)
        {
            
        }
    }    
}