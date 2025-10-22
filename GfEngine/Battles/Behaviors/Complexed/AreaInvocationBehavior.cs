using GfEngine.Battles.Commands;
using GfEngine.Battles.Commands.Advanced;
using GfEngine.Battles.Commands.Core;
using GfEngine.Battles.Patterns;
using GfEngine.Battles.Squares;
using GfEngine.Logics;
using GfEngine.Models.Buffs;
using GfToolkit.Shared;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace GfEngine.Battles.Behaviors.Complexed
{
    // '범위 효과'라는 메커니즘을 책임지는 클래스
    public class AreaInvocationBehavior : Behavior
    {
        public PatternSet Area;
        public int DamageConstant;
        public List<(StatType, float)> Coefficients;
        public DamageType DamageType;
        public BuffSet ApplyingBuffSet;

        // 생성자에서 피해량, 피해 타입, 공격 범위(PatternSet) 등 '데이터'를 받는다.
        public AreaInvocationBehavior() : base()
        {
            Name = ""; // 기본 이름
            DamageConstant = 0; // 기본 피해량
            Coefficients = new List<(StatType, float)>(); // 계수 없음.
            DamageType = DamageType.Physical; // 기본 피해 타입
        }

        public override Command Execute(Square origin, Square target, Square[,] map)
        {
            BundleCommand command = new BundleCommand()
            {
                TargetSquare = target,
                Commands = new List<Command>()
            };
            if (origin.Occupant == null) return command;
            command.Agent = origin.Occupant;
            List<BehaviorTarget> affectedSquares = Area.TargetSearcher(target, map, Accessible);
            foreach (BehaviorTarget bt in affectedSquares)
            {
                if (bt.Type == TargetType.Accessible) // 공격 가능한 칸에 있는 유닛에게만 피해
                {
                    Square sq = map[bt.Y, bt.X];
                    if (sq.Occupant != null)
                    {
                        int damage = DamageConstant;
                        foreach ((StatType, float) iter in Coefficients)
                        {
                            damage += BattleManager.GetModifiedStat(origin.Occupant.GetFinalStatus(), iter.Item1, iter.Item2);
                        }
                        sq.Occupant.CalculateDamage(damage, DamageType);
                        if (damage != 0)
                        {
                            command.Commands.Add(new HitCommand()
                            {
                                Agent = command.Agent,
                                Damage = damage,
                                TargetSquare = target,
                                TargetUnit = target.Occupant
                            });
                        }
                        if (ApplyingBuffSet != null)
                        {
                            command.Commands.Add(new InvocationCommand()
                            {
                                Agent = command.Agent,
                                ApplyingBuffSet = ApplyingBuffSet,
                                TargetSquare = target,
                                TargetUnit = target.Occupant
                            });
                        }
                    }
                }
            }
            return command;
        }
    }
}