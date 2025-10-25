using GfEngine.Battles.Units;
using GfEngine.Logics;
using GfToolkit.Shared;
using System.Collections.Generic;

namespace GfEngine.Battles.Commands.Core
{
    public class HitCommand : Command
    {
        // 이름은 HitCommand지만, 체력 회복 처리에도 사용됨.
        public Unit TargetUnit { get; set; } // 피해를 받는 유닛
        public string Fomula { get; set; } // 피해를 계산할 공식
        public DamageType Type { get; set; } // 해당 피해의 타입. 회복일 경우 DamageType.Heal
        public int Damage; // 실제로 적용되는 피해
        public HitCommand() { }
        public HitCommand(HitCommand parent) : base(parent)
        {
            TargetUnit = parent.TargetUnit;
            Damage = parent.Damage;
        }
        public override void Execute(BattleContext battleContext)
        {
            
        }
        public override Command Clone()
        {
            return new HitCommand(this);
        }
    }    
}