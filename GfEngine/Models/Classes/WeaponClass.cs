using GfEngine.Battles.Patterns;
using GfEngine.Core.Conditions;
namespace GfEngine.Models.Classes
{
    public class WeaponClass : GameObject
    {
        public int BasePatternSet { get; set; }
        public ICondition Accessible { get; set; }
        public ICondition ObstructedBy { get; set; }
        public int Penetration { get; set; }
        public int BaseApCost { get; set; }
        // 근접, 원거리 무기 구분용 플래그
        public bool IsRangeWeapon { get; set; }
    }
}