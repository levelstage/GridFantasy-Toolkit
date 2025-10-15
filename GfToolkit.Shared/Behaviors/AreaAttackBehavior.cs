using GfToolkit.Shared.Battles;
using GfToolkit.Shared.Core;

namespace GfToolkit.Shared.Behaviors
{
    // '범위 공격'이라는 메커니즘을 책임지는 클래스
    public class AreaAttackBehavior : Behavior
    {
        private int Power;
        private DamageType DamageType;
        private PatternSet Area;

        // 생성자에서 피해량, 피해 타입, 공격 범위(PatternSet) 등 '데이터'를 받는다.
        public AreaAttackBehavior() : base()
        {
            Name = ""; // 기본 이름
            Power = 10; // 기본 피해량
            DamageType = DamageType.Physical; // 기본 피해 타입
        }

        public override string Execute(Square origin, Square target, Square[,] map)
        {
            List<BehaviorTarget> affectedSquares = Area.ActionSearcher(target, map, Accessible);
            foreach (BehaviorTarget bt in affectedSquares)
            {
                if (bt.Type == TargetType.Accessible) // 공격 가능한 칸에 있는 유닛에게만 피해
                {
                    Square sq = map[bt.Y, bt.X];
                    if (sq.Occupant != null && Accessible.Contains(GameData.GetTeamType(origin.Occupant.Team, sq.Occupant.Team)))
                    {
                        sq.Occupant.TakeDamage(Power, DamageType);
                    }
                }
            }
            return "";
        }
    }
}