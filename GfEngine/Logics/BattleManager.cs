using GfEngine.Models.Buffs;
using GfToolkit.Shared;
using GfEngine.Models.Statuses;
using GfEngine.Battles.Parsing;
using System;
using System.Collections.Generic;

namespace GfEngine.Logics
{
    public class BattleManager
    {
        // 1. [유지] 싱글턴 진입점 (Static Access Point)
        public static BattleManager Instance { get; set; }

        // 2. [변경] 파서 인스턴스 (Instance Member)
        public IBattleFormulaParser BattleFormulaParser { get; private set; }

        public BattleManager()
        {
            // BattleManager가 생성될 때 파서 인스턴스를 초기화합니다.
            // 이로써 파서는 BattleManager의 상태가 됩니다.
            BattleFormulaParser = new BattleNCalcParser();
            
            // 주의: Unity 환경이라면 Awake()에서 Instance = this;를 호출해야 합니다.
            // Console 환경이라면 최초 사용 시점에 Instance = this;를 호출해야 합니다.
        }

        // 3. [변경] 인스턴스 메서드로 전환
        public float NetBuffMagnitude(BuffEffect effect, List<Buff> buffList)
        {
            float res = 0;
            foreach (Buff bSet in buffList)
            {
                foreach (Modifier iter in bSet.Effects)
                {
                    if (iter.Effect == effect) res += iter.Magnitude;
                }
            }
            return res;
        }

        // 4. [변경] 인스턴스 메서드로 전환
        public int GetParticularStat(Status status, StatType type, float coefficient = 1f)
        {
            float res = 0;
            switch (type)
            {
                case StatType.MaxHp:
                    res = status.MaxHp * coefficient;
                    break;
                case StatType.Defense:
                    res = status.Defense * coefficient;
                    break;
                case StatType.MagicDefense:
                    res = status.MagicDefense * coefficient;
                    break;
                case StatType.Attack:
                    res = status.Attack * coefficient;
                    break;
                case StatType.MagicAttack:
                    res = status.MagicAttack * coefficient;
                    break;
                case StatType.Agility:
                    res = status.Agility * coefficient;
                    break;
                default:
                    res = 0;
                    break;
            }
            return (int)res;
        }
    }
}