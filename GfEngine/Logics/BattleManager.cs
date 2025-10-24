using GfEngine.Models.Buffs;
using GfToolkit.Shared;
using GfEngine.Models.Statuses;
using System;
using System.Collections.Generic;
namespace GfEngine.Logics
{
    public static class BattleManager
    {
        public static float NetBuffMagnitude(BuffEffect effect, List<Buff> buffList)
        {
            // 특정 종류 버프의 magnitude를 모두 합해서 return하는 함수.
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
        public static int GetModifiedStat(Status status, StatType type, float coefficient)
        {
            float res=0;
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