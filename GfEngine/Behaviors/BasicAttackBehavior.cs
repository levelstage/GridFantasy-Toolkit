using GfEngine.Core;
using GfEngine.Models.Items;
using GfEngine.Battles;
using System.Collections.Generic;
using System;
using GfToolkit.Shared;
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
            Accessible = new List<TeamType> { TeamType.Enemy, TeamType.Neutral };
            // 특수한 공격 판정을 가진 무기는 tag를 따로 부여한다.
            Tags = new List<BehaviorTag>();
            if (GameData.SpecialAttacks.ContainsKey(weapon.Type))
            {
                Tags.Add(GameData.SpecialAttacks[weapon.Type]);
            }
            Method = weapon;
        }
        static BasicAttackBehavior counterAttack(Square origin, Square target, int attackCost) // 예상되는 반격을 return하는 함수
        {
            Unit attacker = origin.Occupant;
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

        static int hit(Unit attacker, Unit defender, BasicAttackBehavior B) // attaker가 deffender를 때리는 판정을 하는 함수.
        {
            int damage = B.Method.Power;
            (AttackType atkType, DamageType dmgType) = GameData.AttackDamageTypes[B.Method.Type];
            // 원래라면 B에 있는 여러 태그들을 체크한다. 지금은 없으므로 주석만 달아놓자.
            if (atkType == AttackType.Physical) damage += attacker.LiveStat.Buffed().Attack;
            else damage += attacker.LiveStat.Buffed().MagicAttack;
            return defender.TakeDamage(damage, dmgType); // 가한 피해 return. 
        }

        static (Unit, int, Unit, int, List<BattleTag>) fight(Unit first, Unit last, BasicAttackBehavior B_f, BasicAttackBehavior B_l)
        {
            List<BattleTag> tags = new List<BattleTag>(); // B의 여러 태그들을 확인하고 보고해야할 특이사항들을 tags에 담아서 return.
            int damage_first = hit(first, last, B_f);
            if (last.LiveStat.CurrentHp == 0)
            {
                tags.Add(BattleTag.killedCounter); // 쓰러뜨렸음을 보고한다.
                return (first, damage_first, last, 0, tags); // 패배한 자는 반격할 수 없다.
            }
            int damage_last = hit(last, first, B_l);
            return (first, damage_first, last, damage_last, tags); // 전투 결과를 먼저 공격한 유닛, 피해, 나중에 공격한 유닛, 피해 순으로 전달.
        }

        string explainResult((Unit u1, int d1, Unit u2, int d2, List<BattleTag> tags) battleResult)
        {
            string explainedResult = "";
            Unit firstAttacker = battleResult.u1;
            int damageToSecond = battleResult.d1;
            Unit secondAttacker = battleResult.u2;
            int damageToFirst = battleResult.d2;
            List<BattleTag> tags = battleResult.tags;

            // 선공자의 공격 메시지 추가.
            explainedResult = ">> " + string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_FirstAttack), firstAttacker.Name, secondAttacker.Name, -damageToSecond);
            if (tags.Contains(BattleTag.killedCounter)) // 후공자가 죽었는지?
            {
                explainedResult = explainedResult + "\n>> " + string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_DefenderDied), secondAttacker.Name);
                // = Console.WriteLine($">> {secondAttacker.Name}이(가) 쓰러져 반격할 수 없습니다!");
            }
            else if (tags.Contains(BattleTag.noCounter))
            {
                explainedResult = explainedResult + "\n>> " + string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_DefenderCantCounter), secondAttacker.Name);
                // = Console.WriteLine($">> {secondAttacker.Name}이(가) 반격할 수 없었습니다!");
            }
            else // 일반적인 반격 상황
            {
                explainedResult = explainedResult + "\n>> " + string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_DefenderCantCounter), secondAttacker.Name, -damageToFirst);
                // = Console.WriteLine($">> {secondAttacker.Name}이(가) 반격! {firstAttacker.Name}에게 [{damageToFirst}]의 피해!");
            }

            // 전투 후 최종 HP 상태
            explainedResult = explainedResult + "\n--- " + GameData.Text.Get(GameData.Text.Key.UI_Battle_FinalStateIndicator) + "---";
            // = Console.WriteLine("\n--- 최종 상태 ---");
            explainedResult = explainedResult + "\n" + string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_AttackerFinalState), firstAttacker.Name, firstAttacker.LiveStat.CurrentHp, firstAttacker.LiveStat.Buffed().MaxHp);
            // = Console.WriteLine($"{firstAttacker.Name}: HP {firstAttacker.LiveStat.CurrentHp} / {firstAttacker.LiveStat.Buffed().MaxHp}");
            explainedResult = explainedResult + "\n" + string.Format(GameData.Text.Get(GameData.Text.Key.UI_Battle_AttackerFinalState), secondAttacker.Name, secondAttacker.LiveStat.CurrentHp, secondAttacker.LiveStat.Buffed().MaxHp);
            // = Console.WriteLine($"{secondAttacker.Name}: HP {secondAttacker.LiveStat.CurrentHp} / {secondAttacker.LiveStat.Buffed().MaxHp}");
            return explainedResult;
        }
        public override string Execute(Square origin, Square target, Square[,] map)
        {
            Unit attacker = origin.Occupant;
            Unit defender = target.Occupant;
            BasicAttackBehavior counter = counterAttack(origin, target, this.ApCost);

            if (counter == null)
            {
                int damage = hit(attacker, defender, this);
                List<BattleTag> tags = new List<BattleTag>();
                tags.Add(BattleTag.noCounter);
                return explainResult((attacker, damage, defender, 0, tags));
            }
            (Unit, int, Unit, int, List<BattleTag>) result;
            // 누가 먼저 때릴지, Agility를 비교. 같으면 공격자 우선.
            if (attacker.LiveStat.Buffed().Agility >= defender.LiveStat.Buffed().Agility)
            {
                result = fight(attacker, defender, this, counter);
            }
            else
            {
                result = fight(defender, attacker, counter, this);
            }
            if (attacker.LiveStat.CurrentHp == 0)
            {
                origin.ClearUnit();
            }
            if (defender.LiveStat.CurrentHp == 0)
            {
                origin.ClearUnit();
            }

            return explainResult(result);
        }
    }
}
