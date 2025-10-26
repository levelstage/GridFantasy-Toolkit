using System.Collections.Generic;
using System.Linq;
using GfEngine.Battles.Patterns;
using GfEngine.Models.Actors;
using GfEngine.Models.Buffs;
using GfEngine.Models.Classes;
using GfEngine.Models.Items;
using GfEngine.Models.Statuses;
using GfToolkit.Shared;

namespace GfEngine
{
    public static class GameData
	{
		public static readonly Dictionary<BasicPatternType, PatternSet> BasicPatternSets = new Dictionary<BasicPatternType, PatternSet>
		{
			// 킹(King): 주변 8칸으로 1칸씩 이동
			[BasicPatternType.King] = new PatternSet(new HashSet<Pattern>
			{
				new Pattern { X = -1, Y = -1}, // Up-Left
				new Pattern { X = 0,  Y = -1}, // Up
				new Pattern { X = 1,  Y = -1}, // Up-Right
				new Pattern { X = -1, Y = 0}, // Left
				new Pattern { X = 1,  Y = 0}, // Right
				new Pattern { X = -1, Y = 1}, // Down-Left
				new Pattern { X = 0,  Y = 1}, // Down
				new Pattern { X = 1,  Y = 1}  // Down-Right
			}),

			// 퀸(Queen): 8방향으로 끝까지 이동
			[BasicPatternType.Queen] = new PatternSet(new HashSet<Pattern>
			{
				new VectorPattern { X = -1, Y = -1}, // Up-Left
				new VectorPattern { X = 0,  Y = -1}, // Up
				new VectorPattern { X = 1,  Y = -1}, // Up-Right
				new VectorPattern { X = -1, Y = 0}, // Left
				new VectorPattern { X = 1,  Y = 0}, // Right
				new VectorPattern { X = -1, Y = 1}, // Down-Left
				new VectorPattern { X = 0,  Y = 1}, // Down
				new VectorPattern { X = 1,  Y = 1}  // Down-Right
			}),

			// 비숍(Bishop): 대각선 4방향으로 끝까지 이동
			[BasicPatternType.Bishop] = new PatternSet(new HashSet<Pattern>
			{
				new VectorPattern { X = -1, Y = -1}, // Up-Left
				new VectorPattern { X = 1,  Y = -1}, // Up-Right
				new VectorPattern { X = -1, Y = 1}, // Down-Left
				new VectorPattern { X = 1,  Y = 1}  // Down-Right
			}),

			// 나이트(Knight): 자신을 중심으로 한 반지름 루트5 원 위로 점프
			[BasicPatternType.Knight] = new PatternSet(new HashSet<Pattern>
			{
				new Pattern { X = 1, Y = -2},
				new Pattern { X = 2, Y = -1},
				new Pattern { X = 2, Y = 1},
				new Pattern { X = 1, Y = 2},
				new Pattern { X = -1, Y = 2},
				new Pattern { X = -2, Y = 1},
				new Pattern { X = -2, Y = -1},
				new Pattern { X = -1, Y = -2}
			}),
			// 룩(Rook): 상하좌우 4방향으로 무제한 이동
			[BasicPatternType.Rook] = new PatternSet(new HashSet<Pattern>
			{
				new VectorPattern { X = 1, Y = 0}, // Right
				new VectorPattern { X = 0, Y = 1}, // Down
				new VectorPattern { X = -1, Y = 0}, // Left
				new VectorPattern { X = 0, Y = -1} //Up
			})
		};

		public static readonly Dictionary<int, PatternSet> AllPatternSets = new Dictionary<int, PatternSet>();
		public static readonly Dictionary<int, Buff> AllBuffs = new Dictionary<int, Buff>();
		public static readonly Dictionary<int, Crest> AllCrests = new Dictionary<int, Crest>();
		public static readonly Dictionary<int, WeaponClass> AllWeaponClasses = new Dictionary<int, WeaponClass>();
		public static readonly Dictionary<int, Weapon> AllWeapons = new Dictionary<int, Weapon>();
		public static readonly Dictionary<int, Skill> AllSkills = new Dictionary<int, Skill>();
		public static readonly Dictionary<int, Actor> AllActors = new Dictionary<int, Actor>();

			public static readonly Dictionary<int, Trait> AllTraits = new Dictionary<int, Trait>();
		// 테마별 랜덤 특성 풀 (TraitType별로 구분된 특성들의 집합. static 생성자에서 자동 생성됨.)
		public static readonly Dictionary<TraitType, HashSet<Trait>> ThemedRandomTraitPools = new Dictionary<TraitType, HashSet<Trait>>();
		
		// 특성 희귀도별 가중치 설정
		public static Dictionary<TraitRarity, int> rarityWeights = new Dictionary<TraitRarity, int>
		{
			{ TraitRarity.Common, 90 },
			{ TraitRarity.Rare, 7 },
			{ TraitRarity.Heroic, 3 }
		};


		public static class Text
		{
				// 텍스트의 '고유 키' 역할을 할 Enum (나중에 Index로도 써먹을 수 있을듯.)
				public enum Key
				{
					// 명령어
					Command_Move,
					Command_Attack,
					Command_Cancel,

					// UI 메시지
					UI_SelectSquare,
					UI_ChooseAction,
					UI_InvalidCoordinate,
					UI_PressEnterToContinue,
					// 기본 공격 관련 UI
					UI_Battle_FirstAttack,
					UI_Battle_DefenderDied,
					UI_Battle_DefenderCantCounter,
					UI_Battle_ExecuteCounterAttack,
					UI_Battle_CounterWasFaster,
					UI_Battle_FinalStateIndicator,
					UI_Battle_AttackerFinalState,
					// 기본 공격 외 Behavior 관련 UI
					UI_Behavior_CharacterMoved,
					UI_Behavior_GiveEffect,
					UI_Behavior_GiveEffectWithDamage,

					// 캐릭터 이름
					Actor_Phantom_Name,
					Actor_Hagen_Name,
					Actor_Gideon_Name,
					Actor_Elara_Name,
					Actor_Cassandra_Name,

					// 무기 이름
					Weapon_PhantomShield_Name,
					Weapon_IronShield_Name,
					Weapon_LongSword_Name,
					Weapon_IronSpear_Name,

					// 무기 설명
					Weapon_PhantomShield_Desc,
					Weapon_IronShield_Desc,
					Weapon_LongSword_Desc,
					Weapon_IronSpear_Desc,

					// 스킬 이름
					Skill_Frontline_Name,

					// 버프 이름
					Buff_FrontlineBuff_Name,

					// 버프 설명
					Buff_FrontlineBuff_Desc,


				}

			// 한국어 텍스트 데이터 딕셔너리
			private static readonly Dictionary<Key, string> Korean = new Dictionary<Key, string>
			{
				// 커맨드 관련
				[Key.Command_Move] = "이동",
				[Key.Command_Attack] = "공격",
				[Key.Command_Cancel] = "취소",
				// UI 관련
				[Key.UI_SelectSquare] = "칸을 선택하세요",
				[Key.UI_ChooseAction] = "어떤 행동을 하시겠습니까?",
				[Key.UI_InvalidCoordinate] = "잘못된 좌표입니다.",
				[Key.UI_PressEnterToContinue] = "계속하려면 엔터를 누르세요...",
				// 기본 공격 관련 UI 메시지
				[Key.UI_Battle_FirstAttack] = "{0}, {1}에게 공격! [{2}]의 피해!",
				[Key.UI_Battle_DefenderDied] = "{0}, 쓰러져 공격할 수 없습니다!",
				[Key.UI_Battle_DefenderCantCounter] = "{0}, 반격할 수 없습니다!",
				[Key.UI_Battle_CounterWasFaster] = "{0}, 공격을 간파하고 선제 공격! [{1}]의 피해!",
				[Key.UI_Battle_ExecuteCounterAttack] = "{0}, 반격! [{1}]의 피해!",
				[Key.UI_Battle_FinalStateIndicator] = "전투 결과",
				[Key.UI_Battle_AttackerFinalState] = "{0}: HP {1} / {2}",
				[Key.UI_Behavior_GiveEffect] = "{0}, {1}에게 {2} 상태 부여!",
				[Key.UI_Behavior_GiveEffectWithDamage] = "",
				// 각종 Behavior 관련 UI 메시지
				[Key.UI_Behavior_CharacterMoved] = "{0}, {1}로 이동.",

				// Actor 관련
				[Key.Actor_Phantom_Name] = "환영병",
				[Key.Actor_Hagen_Name] = "하겐",
				[Key.Actor_Gideon_Name] = "기드온",
				[Key.Actor_Elara_Name] = "엘라라",
				[Key.Actor_Cassandra_Name] = "카산드라",
				// Weapon 관련
				// Weapon 이름
				[Key.Weapon_PhantomShield_Name] = "환영 방패",
				[Key.Weapon_IronShield_Name] = "철 방패",
				[Key.Weapon_LongSword_Name] = "롱소드",
				[Key.Weapon_IronSpear_Name] = "쇠창",
				// Weapon 설명
				[Key.Weapon_PhantomShield_Desc] = "환영병의 기본 무장.",
				[Key.Weapon_IronShield_Desc] = "제국의 병사들이 애용하는, 믿음의 철 방패.",
				[Key.Weapon_LongSword_Desc] = "기본에 충실한, 잘 만들어진 검.",
				[Key.Weapon_IronSpear_Desc] = "손잡이도 쇠로 만들어져, 다소 무겁지만 부러질 일은 없다.",
				// Buff 관련
				// Buff 이름
				[Key.Buff_FrontlineBuff_Name] = "전선 지원",
				// Buff 설명
				[Key.Buff_FrontlineBuff_Desc] = "아군의 전선 구축으로 방어력이 증가했습니다.",
				// Skill 관련
				// Skill 이름
				[Key.Skill_Frontline_Name] = "전선 구축",

			};
			
			// 현재 언어 설정 (나중에 옵션에서 바꿀 수 있도록)
			public static Dictionary<Key, string> CurrentLanguage = Korean;

			// 텍스트를 가져오는 함수 (지금은 한국어 데이터만 있긴 하지만.)
			public static string Get(Key key)
			{
				if (CurrentLanguage.ContainsKey(key))
				{
					return CurrentLanguage[key];
				}
				return key.ToString(); // 혹시 데이터가 없으면 키 이름을 그대로 반환
			}
		}
			// 특성을 배우는 레벨과 스킬 강화 레벨
			public static List<int> TraitLevels = new List<int> { 4, 7, 10, 13, 16, 20 };
			public static HashSet<int> SkillUpgradeLevels = new HashSet<int> { 10, 20 };
			public static HashSet<BattleEventType> GlobalEvents = new HashSet<BattleEventType>(){
				BattleEventType.OnTurnStart,
				BattleEventType.OnActionEnd
			};

			static GameData()
			{
				ThemedRandomTraitPools = AllTraits.Values
											.GroupBy(trait => trait.Type)
											.ToDictionary(group => group.Key, group => new HashSet<Trait>(group));
			}
	}
}

