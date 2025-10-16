using GfToolkit.Shared.Core;
using GfToolkit.Shared.Models.Actors; // Trait 클래스를 쓰기 위해 추가
using System.Collections.Generic;
using System.Linq;

namespace GfToolkit.Shared.Logics
{
    public static class TraitManager
    {
         // 가중치 기반 랜덤 특성 뽑기 함수
        private static Trait GetWeightedRandomTrait(HashSet<Trait> pool, HashSet<Trait> antiPool)
        {
            // antiPool에 있는 모든 특성은 선택 제외
            // antiPool: 기보유 특성, 영웅 특성을 이미 가진 경우 모든 영웅 특성, 캐릭터별 금지 특성
            List<Trait> availablePool = pool.Except(antiPool).ToList();
            // 만약 사용 가능한 특성이 없다면 null 반환
            if (availablePool == null || availablePool.Count == 0)
            {
                return null;
            }

            // 필터링된 목록(availablePool)을 가지고 가중치 뽑기를 실행한다.
            int totalWeight = availablePool.Sum(trait => GameData.rarityWeights.GetValueOrDefault(trait.Rarity, 1));
            int randomNumber = RandomManager.GetRandomInt(0, totalWeight);
            foreach (var trait in availablePool)
            {
                int weight = GameData.rarityWeights.GetValueOrDefault(trait.Rarity, 1);
                if (randomNumber < weight)
                {
                    return trait;
                }
                randomNumber -= weight;
            }
            return availablePool.Last();
        }

        // 캐릭터의 특성 덱을 생성하는 함수
        public static Dictionary<int, List<Trait>> GetTraitDeck(Actor character, int startLevel = 4)
        {
            Dictionary<int, List<Trait>> TraitDeck = character.FixedTraits;
            HashSet<Trait> primalAntiPool = new HashSet<Trait>(character.Traits);
            if (character.ForbiddenTraits != null)
            {
                foreach (var trait in character.ForbiddenTraits) primalAntiPool.Add(trait);
            }
            bool hasHeroicTrait = character.Traits.Any(t => t.Rarity == TraitRarity.Heroic);
            ;
            foreach (int level in GameData.TraitLevels)
            {
                if (level < startLevel) continue; // 이미 지난 레벨은 건너뜀

                // 스킬 강화 레벨(10, 20)인지 확인
                if (GameData.SkillUpgradeLevels.Contains(level))
                {
                    // 스킬 강화 레벨이면, 해당 레벨의 스킬 강화 특성들만 반환
                    TraitDeck[level] = character.UniqueSkillTraits[level];
                }
                else
                {
                    // 고정 특성 레벨이면, 해당 레벨의 고정 특성들로 시작
                    List<Trait> choices = new List<Trait>();
                    if (character.FixedTraits != null && character.FixedTraits.ContainsKey(level))
                    {
                        choices.AddRange(character.FixedTraits[level]);
                    }
                    // 첫 번째 고정 특성의 타입을 그 레벨의 테마로 간주
                    TraitType theme = choices[0].Type;
                    // 테마에 맞는 랜덤 풀(Pool) 가져오기
                    if (GameData.ThemedRandomTraitPools.TryGetValue(theme, out HashSet<Trait> randomPool))
                    {
                        HashSet<Trait> antiPool = new HashSet<Trait>(primalAntiPool);
                        // 영웅 특성을 이미 가진 경우, 영웅 특성은 뽑지 않도록 함
                        if (hasHeroicTrait) antiPool.UnionWith(randomPool.Where(t => t.Rarity == TraitRarity.Heroic));
                        Trait randomTrait = GetWeightedRandomTrait(randomPool, antiPool);
                        if (randomTrait != null)
                        {
                            choices.Add(randomTrait);
                        }
                    }
                }
            }
            return TraitDeck;
        }
    }
}