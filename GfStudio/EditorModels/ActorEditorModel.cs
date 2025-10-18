public class ActorEditorModel
{
    public int Code { get; set; } = 0;
    public string Name { get; set; } = "";

    // 초기 스테이터스
    public int MaxHP { get; set; }
    public int Defense { get; set; }
    public int MagicDefense { get; set; }
    public int Attack { get; set; }
    public int MagicAttack { get; set; }
    public int Agility { get; set; }

    // 기본 성장치
    public int GrowthMaxHP { get; set; }
    public int GrowthDefense { get; set; }
    public int GrowthMagicDefense { get; set; }
    public int GrowthAttack { get; set; }
    public int GrowthMagicAttack { get; set; }
    public int GrowthAgility { get; set; }

    // 사용 무기군
    public string MoveType { get; set; } = "";
    public string WeaponType { get; set; } = "";

    // 스킬 / 장비 / 인벤토리
    public string UniqueSkillCode { get; set; } = "";
    public string EquipmentCode { get; set; } = "";
    public List<string> InventoryCodes { get; set; } = new();

    // 특성
    public List<string> FixedTraitCodes { get; set; } = new();
    public List<string> ForbiddenTraitCodes { get; set; } = new();
}
