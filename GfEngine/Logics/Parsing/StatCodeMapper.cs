using GfToolkit.Shared;
using System.Collections.Generic;

namespace GfEngine.Logics.Parsing
{
    // 정적인 매핑 정보를 담는 클래스
    public static class StatCodeMapper
    {
        // 런타임에 단 한 번만 생성하여 성능을 확보
        private static readonly Dictionary<StatType, string> StatCodeMap = new Dictionary<StatType, string>
        {
            // 체력/방어
            { StatType.MaxHp, "MHP" },
            { StatType.CurrentHp, "HP" }, // CurrentHp는 HP로 명시
            { StatType.Defense, "DEF" },
            { StatType.MagicDefense, "MDF" },

            // 공격
            { StatType.Attack, "ATK" },
            { StatType.MagicAttack, "MAT" },

            // 기타
            { StatType.Agility, "AGI" },
            // ... 새 스탯 추가 시 여기에만 추가
        };

        // StatType을 받아 약어 문자열을 반환하는 메서드
        public static string GetCode(StatType statType)
        {
            if (StatCodeMap.TryGetValue(statType, out string code))
            {
                return code;
            }
            
            // 매핑이 정의되지 않은 스탯이 들어왔을 때의 안전장치
            // (예: Enum 이름을 그대로 사용하거나, 예외 발생)
            throw new KeyNotFoundException($"StatType '{statType}'에 대한 약어 코드가 StatCodeMapper에 정의되어 있지 않습니다.");
        }
    }
}