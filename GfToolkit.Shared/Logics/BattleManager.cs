using GfToolkit.Shared.Models.Buffs;
namespace GfToolkit.Shared.Logics
{
    public static class BattleManager
    {
        public static float NetBuffMagnitude(BuffType type, List<BuffSet> buffList)
        {
            // 특정 종류 버프의 magnitude를 모두 합해서 return하는 함수.
            float res = 0;
            foreach (BuffSet bSet in buffList)
            {
                foreach (Buff iter in bSet.Effects)
                {
                    if (iter.Type == type) res += iter.Magnitude;
                }
            }
            return res;
        }
    }
}