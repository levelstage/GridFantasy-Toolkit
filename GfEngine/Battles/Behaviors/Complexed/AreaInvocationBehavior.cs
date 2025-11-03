using GfEngine.Battles.Commands;
using GfEngine.Battles.Commands.Advanced;
using GfEngine.Battles.Commands.Core;
using GfEngine.Battles.Parsing;
using GfEngine.Battles.Patterns;
using GfEngine.Battles.Rules;
using GfEngine.Battles.Squares;
using GfEngine.Logics;
using GfToolkit.Shared;
using System;
using System.Collections.Generic;

namespace GfEngine.Battles.Behaviors.Complexed
{
    // '범위 효과'라는 메커니즘을 책임지는 클래스
    public class AreaInvocationBehavior : Behavior
    {
        public RuledPatternSet Area; // 대상을 검색할 영역
        public List<ConditionalCommandRule> Invocations { get; set; }
        // 해당 범위에서 검색된 대상들을 가지고 어떤 Command를 만들지.
        // 이 List 안에 있는 Command들은 일부 속성이 비어있는 불완성품임.
        // 여기에 적절한 속성을 채우고, BundleCommand화해서 Return하는게 이 Behavior의 역할이다.
        public override Command Execute(BattleContext context)
        {
            Square target = context.TargetSquare;
            Square[,] map = context.WaveData.Map;

            if (context.OriginUnit == null) return new NullCommand();
            BundleCommand res = new BundleCommand()
            {
                SourceUnit = context.OriginUnit,
                TargetSquare = target,
                Commands = new List<Command>()
            };
            // 2차 검사: 영역 내에서 2차 대상으로 지정이 가능한지?
            List<BehaviorTarget> affectedSquares = Area.GetValidTargets(target, context.WaveData);
            foreach (BehaviorTarget bt in affectedSquares)
            {
                if (bt.Type == TargetType.Accessible)
                {
                    Square subTarget = map[bt.Y, bt.X];
                    if (subTarget.Occupant != null)
                    {
                        BattleContext subContext = new BattleContext(
                            waveData: context.WaveData,
                            originSquare: context.OriginSquare,
                            targetSquare: subTarget
                        );
                        foreach(var iter in Invocations)
                        {
                            // 해당 Behavior에서 처리할 수 있는 Command의 종류는 다음과 같음:
                            // HitCommand (유닛의 체력 변동)
                            // GrantBuffCommand (유닛에게 Buff/Debuff 부여)
                            // ApplyGroundEffectCommand (Square에 GroundEffect 부여)
                            // 이론상 거의 모든 Command를 넣을 수 있다.

                            // 3차 검사: 해당 Command를 실행할 수 있는지?
                            // 해당 검사를 통과한 커맨드들은 내용을 조립해준다.
                            if (iter.Condition.IsMet(subContext))
                            {
                                Command subCommand = iter.CommandToExecute.Clone();
                                if (subCommand is HitCommand hc)
                                {
                                    hc.SourceUnit = context.OriginUnit;
                                    hc.TargetSquare = subTarget;
                                    hc.TargetUnit = subTarget.Occupant;
                                    IBattleFormulaParser parser = BattleManager.Instance.BattleFormulaParser;
                                    // 방어력, 저항력 등을 계산하기 이전의 데미지
                                    int primalDamage = (int)parser.Evaluate(hc.Fomula, subContext);
                                    // 이 커맨드의 진정한 데미지.
                                    hc.Damage = subTarget.Occupant.CalculateDamage(primalDamage, hc.Type);
                                }
                                else if (subCommand is GrantingBuffCommand gbc)
                                {
                                    gbc.SourceUnit = context.OriginUnit;
                                    gbc.TargetSquare = subTarget;
                                    gbc.TargetUnit = subTarget.Occupant;
                                }
                                else if (subCommand is ApplyGroundEffectCommand agec)
                                {
                                    agec.SourceUnit = context.OriginUnit;
                                    agec.TargetSquare = subTarget;
                                }
                                // 정해진 종류의 커맨드 이외에는 예외로 던져버린다.
                                else
                                {
                                    throw new InvalidOperationException(
                                        $"Behavior '{Name}' tried to generate an unknown Command type: {subCommand.GetType().Name}. " +
                                        "Check ConditionalCommandRule data."
                                    );
                                }                                
                                // 조립된 커맨드를 bundle에 포장한다.
                                res.Commands.Add(subCommand);
                            }
                        }
                    }
                }
            }
            return res;
        }
    }
}