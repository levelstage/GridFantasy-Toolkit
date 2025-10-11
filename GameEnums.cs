public enum BasicPatternType // 체스의 다섯 기물의 패턴을 저장해둔 dictioanry에 접근하기 위한 keys.
{
	King, Knight, Bishop, Queen, Rook
}

public enum MoveType // 캐릭터에게 주어질 수 있는 모든 종류의 이동 방법
{
	Stationary, King, Pawn_Up, Pawn_Up_Advanced, Pawn_Down, Pawn_Down_Advanced, Knight, Bishop, Rook, Queen
}

public enum WeaponType // 캐릭터에게 주어질 수 있는 모든 종류의 무기의 식별번호 역할을 함.
{
	Sword, Shield_Up, Shield_Down, Spear, Cannon, Bow, Fireball
}

public enum AttackType // 공격이 공격력을 참고하는지, 마법 공격력을 참고하는지 확인. 나중에 방어력 참고 공격 이런것도 생각중.
{
	Physical, Magical
}

public enum DamageType // 피해 유형. 속성 피해 등을 추가할수도 있다.
{
	Physical, Magical, True
}

public enum BuffType // 각 버프의 실질적 효과를 식별하기 위한 타입. 같은 타입의 버프들은 중첩된다.
{
	MaxHpBoost,DefenseBoost,MagicDefenseBoost, AttackBoost, MagicAttackBoost, AgilityBoost, PhysicalResidence, MagicalResidence
}

public enum Teams // 말 그대로 어느 소속인지
{
    Players,    // 플레이어 소속
    Enemies,    // 적 1 소속
    Neutrals    // 중립
}

public enum TeamType // 출처와 대상의 관계. 이 대상이 어떤 behavior의 대상이 되는지를 확인하기 위한 타입.
{
	Same, Ally, Neutral, Enemy, Air
}

public enum TerrainType // 지형의 종류를 구분하기 위한 타입.
{
	Plains, Swamp, Water
}

public enum PatternType // PatternSet에 들어있는 Pattern이 좌표인지 벡터인지 구분하기 위한 타입.
{ 
	Coordinate, Vector, Pvector
}

public enum ActionType // 검색된 액션이 유효한지 아닌지 알려주기 위한 타입.
{
	Accessible, Unaccessible
}

public enum BehaviorTag // 해당 Behavior의 특성을 세세하게 설명하는 태그.
{
	PawnFirstUp, PawnFirstDown
}

public enum BattleTag // 전투 결과에 대한 상세 설명에 필요한 태그. 크리티컬을 추가한다면 크리티컬 발생 여부나 뭐 이런거.
{
	noCounter, killedCounter
}

public enum ItemTag
{

}

public enum WeaponTag
{

}

public enum ActorCode // 각각의 Actor들의 고유번호.
{
	Phantom, Hagen, Gideon, 
}

public enum WeaponCode // 각각의 Weapon들의 고유번호
{
	PhantomShield, IronShield, LongSword, IronSpear
}

public enum SkillCode
{
	Frontline
}

public enum BuffSetCode
{
	FrontlineAura, FrontlineBuff
}

//Enum 끝.