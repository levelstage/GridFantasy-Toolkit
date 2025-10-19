using GfEngine.Models.Buffs;
using GfToolkit.Shared;
using System.Collections.Generic;
namespace GfEngine.Logics
{
    public static class BattleManager
    {
        public static float NetBuffMagnitude(BuffEffect effect, List<BuffSet> buffList)
        {
            // 특정 종류 버프의 magnitude를 모두 합해서 return하는 함수.
            float res = 0;
            foreach (BuffSet bSet in buffList)
            {
                foreach (Buff iter in bSet.Effects)
                {
                    if (iter.Effect == effect) res += iter.Magnitude;
                }
            }
            return res;
        }
    }
}