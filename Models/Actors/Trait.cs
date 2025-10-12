using GfEngine.Models.Buffs;
using System;

namespace GfEngine.Models.Actors
{
    public class Trait : IEquatable<Trait>
    {
        public TraitCode Code { get; set; } // 특성 고유 ID (예: "Hagen_U_01")
        public string Name { get; set; }
        public string Description { get; set; }

        // 이 특성이 어떤 종류인지 구분 (궁극기 강화용인지, 일반인지 등)
         public TraitType Type { get; set; }

        // 이 특성의 희귀도
        public TraitRarity Rarity { get; set; }

        // 이 특성을 선택했을 때 얻게 되는 실제 효과 (버프)
        public BuffSet TraitBuff { get; set; }

        public Trait()
        {
            // 기본값은 Common으로 설정해서, 희귀 특성만 따로 지정해주면 편해.
            this.Rarity = TraitRarity.Common;
        }
        public bool Equals(Trait other)
        {
            if (other == null) return false;
            return this.Code == other.Code;
        }
        public override int GetHashCode()
        {
            return this.Code.GetHashCode();
        }
    
    }
}