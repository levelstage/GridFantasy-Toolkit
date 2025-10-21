using GfEngine.Core;
using GfEngine.Models.Items;
using GfEngine.Battles;
using System.Collections.Generic;
using System;
using GfToolkit.Shared;
using GfEngine.Behaviors.BehaviorResults;
namespace GfEngine.Behaviors
{
    public class BasicAttackBehavior : Behavior
    {
        public Weapon Method;
        public BasicAttackBehavior(Weapon weapon) // 공격 behavior 생성자
        {
            Name = GameData.Text.Get(GameData.Text.Key.Command_Attack);
            Scope = GameData.AttackPatterns[weapon.Type];
            ApCost = GameData.AttackApCosts[weapon.Type];
            Accessible = new HashSet<TeamType> { TeamType.Enemy, TeamType.Neutral };
            // 특수한 공격 판정을 가진 무기는 tag를 따로 부여한다.
            Tags = new HashSet<BehaviorTag>();
            if (GameData.SpecialAttacks.ContainsKey(weapon.Type))
            {
                Tags.Add(GameData.SpecialAttacks[weapon.Type]);
            }
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
                foreach (Pattern p in B.Scope.Patterns)

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

        private static int Hit(Unit attacker, Unit defender, BasicAttackBehavior B) // attaker가 deffender를 때리는 판정을 하는 함수.
        {
            int damage = B.Method.Power;
            (AttackType atkType, DamageType dmgType) = GameData.AttackDamageTypes[B.Method.Type];
            // 원래라면 B에 있는 여러 태그들을 체크한다. 지금은 없으므로 주석만 달아놓자.
            if (atkType == AttackType.Physical) damage += attacker.LiveStat.Buffed().Attack;
            else damage += attacker.LiveStat.Buffed().MagicAttack;
            return -defender.TakeDamage(damage, dmgType); // 가한 피해 = -체력 변동
        }

        public override BehaviorResult Execute(Square origin, Square target, Square[,] map)
        {
            Unit attacker = origin.Occupant;
            Unit defender = target.Occupant;
            BasicAttackBehavior counter = GetCounterAttack(origin, target, this.ApCost);
            BasicAttackResult res = new BasicAttackResult() { Agent = attacker, Tags = new HashSet<BattleTag>(), TargetSquare = target, Victim = defender};
            if (counter == null)
            {
                res.Damage = Hit(attacker, defender, this);
                res.Tags.Add(BattleTag.noCounter);
                res.HadInitiative = true;
                res.CounterAttackResult = null;
                return res;
            }
            (Unit, int, Unit, int, HashSet<BattleTag>) result;
            // 누가 먼저 때릴지, Agility를 비교. 같으면 공격자 우선.
            if (attacker.LiveStat.Buffed().Agility >= defender.LiveStat.Buffed().Agility)
            {
                // 공격자의 선공
                res.Damage = Hit(attacker, defender, this);
                res.HadInitiative = true;
                if (defender.LiveStat.CurrentHp == 0)
                {
                    res.CounterAttackResult = null;
                    res.Tags.Add(BattleTag.noCounter);
                    res.Tags.Add(BattleTag.killedCounter);
                }
                else
                {
                    AttackResult counterAttackResult = new AttackResult() { Agent = defender, Tags = new HashSet<BattleTag>(), Victim = attacker, TargetSquare = origin};
                    counterAttackResult.Damage = Hit(defender, attacker, counter);
                    res.CounterAttackResult = counterAttackResult;
                }
            }
            else
            {
                // 수비자의 선공
                AttackResult counterAttackResult = new AttackResult() { Agent = defender, Tags = new HashSet<BattleTag>(), Victim = attacker, TargetSquare = origin};
                counterAttackResult.Damage = Hit(defender, attacker, counter);
                res.HadInitiative = false;
                if (attacker.LiveStat.CurrentHp == 0)
                {
                    res.Damage = 0;
                    counterAttackResult.Tags.Add(BattleTag.noCounter);
                    counterAttackResult.Tags.Add(BattleTag.killedCounter);
                    res.CounterAttackResult = counterAttackResult;
                }
                else
                {
                    res.Damage = Hit(attacker, defender, this);
                    res.CounterAttackResult = counterAttackResult;
                }
            }
            if (attacker.LiveStat.CurrentHp == 0)
            {
                origin.ClearUnit();
            }
            if (defender.LiveStat.CurrentHp == 0)
            {
                origin.ClearUnit();
            }

            return res;
        }
    }
}
