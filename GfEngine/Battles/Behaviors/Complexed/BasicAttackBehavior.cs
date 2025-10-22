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
namespace GfEngine.Battles.Behaviors.Complexed
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
            int damage = 0;
            (AttackType atkType, DamageType dmgType) = GameData.AttackDamageTypes[B.Method.Type];
            if (atkType == AttackType.Physical) damage += attacker.GetFinalStatus().Attack;
            else damage += attacker.GetFinalStatus().MagicAttack;
            damage = defender.CalculateDamage((int)(damage * B.Method.Power), dmgType);
            // 오버데미지 방지
            if (damage > defender.CurrentHp()) return defender.CurrentHp(); 
            return damage;
        }

        public override Command Execute(Square origin, Square target, Square[,] map)
        {
            Unit attacker = origin.Occupant;
            Unit defender = target.Occupant;
            BasicAttackCommand res = new BasicAttackCommand()
            {
                Agent = attacker,
                TargetSquare = target,
                TargetUnit = defender,
                Incidents = new List<Command>()
            };
            if (attacker == null || defender == null) return res;            
            BasicAttackBehavior counter = GetCounterAttack(origin, target, this.ApCost);
            if (counter == null)
            {
                // 카운터가 없을 시 그냥 때림.
                res.Damage = Hit(attacker, defender, this);
                res.Incidents.Add(new HitCommand(res));
                // "가시" 혹은 "흡혈" 등을 처리해야함. 처리하고 반드시 Incidents에 넣을것.
                res.HadInitiative = true; // 일단 선공권을 가진 것으로 간주.
                res.CounterAttackCommand = null;
                return res;
            }
            // 누가 먼저 때릴지, Agility를 비교. 같으면 공격자 우선.
            if (attacker.GetFinalStatus().Agility >= defender.GetFinalStatus().Agility)
            {
                // 공격자의 선공
                res.Damage = Hit(attacker, defender, this);
                res.Incidents.Add(new HitCommand(res));
                // "가시" 혹은 "흡혈" 등을 처리해야함. 처리하고 반드시 Incidents에 넣을것.
                res.HadInitiative = true;
                HitCommand counterAttackCommand = new HitCommand() { Agent = defender, TargetUnit = attacker, TargetSquare = origin};
                counterAttackCommand.Damage = Hit(defender, attacker, counter);
                res.CounterAttackCommand = counterAttackCommand;
                res.Incidents.Add(counterAttackCommand);
                // 마찬가지로 "가시" 혹은 "흡혈" 등을 처리해야함. 처리하고 반드시 Incidents에 넣을것.
            }
            else
            {
                // 수비자의 선공
                HitCommand counterAttackCommand = new HitCommand() { Agent = defender, TargetUnit = attacker, TargetSquare = origin};
                counterAttackCommand.Damage = Hit(defender, attacker, counter);
                res.CounterAttackCommand = counterAttackCommand;
                res.Incidents.Add(new HitCommand(res));
                // "가시" 혹은 "흡혈" 등을 처리해야함. 처리하고 반드시 Incidents에 넣을것.
                res.HadInitiative = false;
                res.Damage = Hit(attacker, defender, this);
                res.Incidents.Add(new HitCommand(res));
                // 마찬가지로 "가시" 혹은 "흡혈" 등을 처리해야함. 처리하고 반드시 Incidents에 넣을것.
            }
            return res;
        }
    }
}
