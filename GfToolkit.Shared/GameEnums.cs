namespace GfToolkit.Shared
{
	// Type들
	public enum BasicPatternType // 기본적인 패턴을 저장해둔 dictioanry에 접근하기 위한 keys.
	{
		King, Knight, Bishop, Queen, Rook
	}

	public enum MoveType // 캐릭터에게 주어질 수 있는 모든 종류의 이동 방법
	{
		Stationary, King, Pawn_Up, Pawn_Up_Advanced, Pawn_Down, Pawn_Down_Advanced, Knight, Bishop, Rook, Queen
	}

	public enum WeaponType // 캐릭터에게 주어질 수 있는 모든 무기군.
	{
		Sword, Shield_Up, Shield_Down, Spear, Cannon, Bow, MagicMissile
	}

	public enum AttackType // 기본 공격이 공격력을 참고하는지, 마법 공격력을 참고하는지 확인. 나중에 방어력 참고 공격 이런것도 생각중.
	{
		Physical, Magical
	}

	public enum DamageType // 피해 유형. 속성 피해 등을 추가할수도 있다.
	{
		Physical, // 대부분의 무기의 물리 피해.
		Magical, // 마법 피해.
		True, // 고정 피해.
		Heal // 회복 피해. 받는 회복량 증가 등에 영향 받음.
	}

	public enum BuffEffect // 각 버프의 실질적 효과를 식별하기 위한 타입. 같은 타입의 버프들은 중첩된다.
	{
		// 정량적 버프들

		// 기본 6스탯 관련
		MaxHpBoost,
		DefenseBoost,
		MagicDefenseBoost,
		AttackBoost,
		MagicAttackBoost,
		AgilityBoost,
		//저항력 관련
		PhysicalResidence,
		MagicalResidence,
		HealResidence,
		// 관통력
		AddMovePenetration,
		AddWeaponPenetration,
		// 범위 연장
		ExtendMoveLength,
		ExtendWeaponLength,

		// 정성적 버프들
		// Aura 관련
		Aura,
		// ConditionOverrider 관련
		OverrideMoveObstruction,
		OverrideMoveAcceibles,
		OverrideWeaponObstruction,
		OverrideWeaponAccessibles,
		// PatternSetOverrider 관련
		OverrideMovePatternSet,
		OverrideWeaponPatternSet,
		// FomulaOverrider 관련
		OverrideBasicAttackFomula,
		// 피해 유형 변경
		ChangeBasicAttackDamageType,
	}

	public enum Relation // 출처와 대상의 관계. 이 대상이 어떤 behavior의 대상이 되는지를 확인하기 위한 타입.
	{
		Self, Ally, Neutral, Enemy, Air
	}

	public enum TerrainType // 지형의 종류를 구분하기 위한 타입.
	{
		Plains, Swamp, Water
	}
	public enum TargetType // 검색된 액션이 유효한지 아닌지 알려주기 위한 타입.
	{
		Accessible, Unaccessible
	}

	public enum StatType // Status의 각 속성들을 식별하기 위한 타입.
	{
		MaxHp, Defense, MagicDefense, Attack, MagicAttack, Agility, CurrentHp
	}

	public enum TraitType
	{
		Unique,     // 캐릭터 고유의 스킬 강화
		Aura,       // 오라 관련
		Tank,       // 방어/생존 관련
		Damage,     // 공격 관련
		Utility     // 유틸리티 관련
	}

	// 전투 중에 발생하는 모든 트리거들의 모음
	public enum BattleEventType
	{
		// 턴 및 행동 흐름
		OnTurnStart,        // 유닛의 턴이 시작될 때
		OnActionEnd,        // 유닛이 행동을 완료했을 때

		// 피해 및 회복
		OnDamaged,          // 유닛이 피해를 입었을 때 (반격, 분노 발동)
		OnDealDamage,       // 유닛이 피해를 주었을 때 (흡혈, 추가 타격)
		OnHealed,           // 유닛이 회복했을 때

		// 상태 변화
		OnDied,             // 유닛이 사망했을 때
		OnBuffApplied,      // 버프가 적용될 때
		OnStatusChanged     // HP가 50% 이하로 떨어지는 등 상태가 변했을 때
	}
	// EventialBehavior의 대상 지정 종류
	public enum EventialTarget
	{
		Self,
		CommandSource,
		CommandTarget,
		System
	}

	// 시스템 구현을 위해 필요한 enum
	public enum ComparisonOperator // 비교형 Condition을 위한 연산자 열거
	{
		GreaterThan,       // >
		GreaterThanOrEqual, // >=
		LessThan,           // <
		LessThanOrEqual,    // <=
		Equal,              // ==
		NotEqual            // !=
	}
	public enum OverridingOperator
	{
		Rewrite,
		Or,
		And,
		AndNot
	}

	// 기타

	public enum Teams // 말 그대로 어느 소속인지
	{
		Players,    // 플레이어 소속
		Enemies,    // 적 1 소속
		Neutrals    // 중립
	}
	public enum PortraitEmotion // 캐릭터 초상화의 감정 상태
	{
		Normal,   // 기본 표정
		Happy,    // 기쁨
		Sad,      // 슬픔
		Angry,    // 분노
		Surprised // 놀람
	}

	public enum TraitRarity // Trait의 희귀도
	{
		Common, Rare, Heroic, Mythic
	}
}


