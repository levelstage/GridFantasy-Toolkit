using GfEngine.Battles.Units;
using GfToolkit.Shared;
using System.Collections.Generic;

namespace GfEngine.Battles.Commands.Core
{
    public class HitCommand : Command
    {
        // 이름은 HitCommand지만, 체력 회복 처리에도 사용됨.
        public Unit TargetUnit; // 피해를 받는 유닛
        public int Damage; // 피해 그 자체. 음수면 회복이다.
        public HitCommand() { }
        public HitCommand(HitCommand parent) : base(parent)
        {
            TargetUnit = parent.TargetUnit;
            Damage = parent.Damage;
        }
    }    
}