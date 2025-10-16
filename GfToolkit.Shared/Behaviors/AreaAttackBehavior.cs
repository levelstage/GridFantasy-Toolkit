using GfToolkit.Shared.Battles;
using GfToolkit.Shared.Models.Buffs;

namespace GfToolkit.Shared.Behaviors
{
    // '범위 공격'이라는 메커니즘을 책임지는 클래스
    public class AreaAttackBehavior : Behavior
    {
        public PatternSet Area;
        public int Power;
        public DamageType DamageType;
        public BuffSet AppliedBuffSet;

        // 생성자에서 피해량, 피해 타입, 공격 범위(PatternSet) 등 '데이터'를 받는다.
        public AreaAttackBehavior() : base()
        {
            Name = ""; // 기본 이름
            Power = 10; // 기본 피해량
            DamageType = DamageType.Physical; // 기본 피해 타입
        }

        public override string Execute(Square origin, Square target, Square[,] map)
        {
            List<BehaviorTarget> affectedSquares = Area.TargetSearcher(target, map, Accessible);
            foreach (BehaviorTarget bt in affectedSquares)
            {
                if (bt.Type == TargetType.Accessible) // 공격 가능한 칸에 있는 유닛에게만 피해
                {
                    Square sq = map[bt.Y, bt.X];
                    if (sq.Occupant != null)
                    {
                        sq.Occupant.TakeDamage(Power, DamageType);
                        sq.Occupant.LiveStat.Buffs.Add(new BuffSet(AppliedBuffSet));
                    }
                }
            }
            return "";
        }
    }
}