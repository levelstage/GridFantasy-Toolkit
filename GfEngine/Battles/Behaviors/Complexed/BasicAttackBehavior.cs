using GfEngine.Models.Items;
using GfEngine.Battles.Patterns;
using GfEngine.Battles.Units;
using GfEngine.Battles.Squares;
using GfEngine.Battles.Commands;
using GfEngine.Battles.Commands.Advanced;
using GfEngine.Battles.Commands.Core;
using System.Collections.Generic;
using System;
using GfToolkit.Shared;
using GfEngine.Core.Conditions;
using GfEngine.Battles.Conditions;
using GfEngine.Logics;
namespace GfEngine.Battles.Behaviors.Complexed
{
    public class BasicAttackBehavior : Behavior
    {
        public Weapon Method;
        public BasicAttackBehavior(Weapon weapon) // 공격 behavior 생성자
        {
            Name = GameData.Text.Get(GameData.Text.Key.Command_Attack);
            Scope = new RuledPatternSet(
                GameData.AttackPatterns[weapon.Type],
                // 적 또는 중립 공격 가능
                accessible: new OrCondition
                (
                    new List<ICondition>()
                    {
                        new BattleComparingCondition(BattleManager.Instance.BattleFormulaParser, "Relation()", "2", ComparisonOperator.Equal),
                        new BattleComparingCondition(BattleManager.Instance.BattleFormulaParser, "Relation()", "3", ComparisonOperator.Equal)
                    }
                )
            );
            ApCost = GameData.AttackApCosts[weapon.Type];
            Method = weapon;
        }
        static BasicAttackBehavior GetCounterAttack(Square origin, Square target, int attackCost) // 예상되는 반격을 return하는 함수
        {
            Unit defender = target.Occupant;
            int dx = origin.X - target.X, dy = origin.Y - target.Y;
            foreach (Behavior B in defender.Behaviors)
            {
                if (B is not BasicAttackBehavior) continue;
                if (B.ApCost > attackCost) continue;
                foreach (Pattern p in B.Scope.PatternSetToUse.Patterns)

                    if (p is VectorPattern)
                    {
                        // 벡터 처리
                        if (dx * p.Y == dy * p.X) // 벡터 방향 검사
                        {
                            if (Math.Sign(dx) == Math.Sign(p.X) && Math.Sign(dy) == Math.Sign(p.Y)) // 부호까지 검사
                            {
                                return B as BasicAttackBehavior; // 같은 방향, 같은 부호 => 반격 가능
                            }
                        }
                    }
                    else
                    {
                        // 좌표 처리
                        if (p.X == dx && p.Y == dy) return B as BasicAttackBehavior; // 해당 좌표가 공격 범위 내에 있으므로 반격 가능
                    }
            }
            return null;
        }

        private static int Hit(Unit attacker, Unit defender, BasicAttackBehavior B)
        {
            // 공격/반격의 피해를 계산하는 함수
            int damage = 0;
            (AttackType atkType, DamageType dmgType) = GameData.AttackDamageTypes[B.Method.Type];
            if (atkType == AttackType.Physical) damage += attacker.GetFinalStatus().Attack;
            else damage += attacker.GetFinalStatus().MagicAttack;
            damage = defender.CalculateDamage((int)(damage * B.Method.Power), dmgType);
            // 오버데미지 방지
            if (damage > defender.CurrentHp()) return defender.CurrentHp();
            return damage;
        }

        public override Command Execute(BattleContext context)
        {
            Square attackingSquare = context.OriginSquare;
            Square defendingSquare = context.TargetSquare;
            Unit attacker = context.OriginUnit;
            Unit defender = context.TargetUnit;
            HitCommand attack = new HitCommand()
            {
                Agent = attacker,
                TargetSquare = defendingSquare,
                TargetUnit = defender
            };
            BasicAttackCommand res = new BasicAttackCommand()
            {
                Agent = attacker,
                TargetSquare = defendingSquare,
                Commands = new List<Command>(),
                AttackCommand = attack
            };
            if (attacker == null || defender == null) return res;
            BasicAttackBehavior counter = GetCounterAttack(attackingSquare, defendingSquare, ApCost);
            if (counter == null)
            {
                // 카운터가 없을 시 그냥 때림.
                attack.Damage = Hit(attacker, defender, this);
                res.Commands.Add(attack);
                // "가시" 혹은 "흡혈" 등을 처리해야함. 처리하고 반드시 Incidents에 넣을것.
                res.HadInitiative = true; // 일단 선공권을 가진 것으로 간주.
                res.CounterAttackCommand = null;
                return res;
            }
            // 누가 먼저 때릴지, Agility를 비교. 같으면 공격자 우선.
            if (attacker.GetFinalStatus().Agility >= defender.GetFinalStatus().Agility)
            {
                // 공격자의 선공
                attack.Damage = Hit(attacker, defender, this);
                res.Commands.Add(attack);
                // "가시" 혹은 "흡혈" 등을 처리해야함. 처리하고 반드시 Incidents에 넣을것.
                res.HadInitiative = true;
                HitCommand counterAttackCommand = new HitCommand() { Agent = defender, TargetUnit = attacker, TargetSquare = attackingSquare };
                counterAttackCommand.Damage = Hit(defender, attacker, counter);
                res.CounterAttackCommand = counterAttackCommand;
                res.Commands.Add(counterAttackCommand);
                // 마찬가지로 "가시" 혹은 "흡혈" 등을 처리해야함. 처리하고 반드시 Incidents에 넣을것.
            }
            else
            {
                // 수비자의 선공
                HitCommand counterAttackCommand = new HitCommand() { Agent = defender, TargetUnit = attacker, TargetSquare = attackingSquare };
                counterAttackCommand.Damage = Hit(defender, attacker, counter);
                res.CounterAttackCommand = counterAttackCommand;
                res.Commands.Add(counterAttackCommand);
                // "가시" 혹은 "흡혈" 등을 처리해야함. 처리하고 반드시 Incidents에 넣을것.
                res.HadInitiative = false;
                attack.Damage = Hit(attacker, defender, this);
                res.Commands.Add(attack);
                // 마찬가지로 "가시" 혹은 "흡혈" 등을 처리해야함. 처리하고 반드시 Incidents에 넣을것.
            }
            return res;
        }
    }
}
