using GfEngine.Battles.Behaviors;
using GfEngine.Battles.Behaviors.Complexed;
using GfEngine.Models.Actors;
using GfEngine.Models.Items;
using GfEngine.Models.Statuses;
using GfEngine.Models.Buffs;
using System.Collections.Generic;
using GfEngine.Logics;
using GfToolkit.Shared;
namespace GfEngine.Battles.Units
{
	public abstract class Unit
	{
		public string Name { get; set; }
		public LiveStatus LiveStat { get; set; }
		public Teams Team { get; set; }
		public List<Behavior> Behaviors { get; set; }

		private void InitializeBehaviors()
		{
			// 행동 리스트 초기화
			Behaviors = new List<Behavior>();
			// 기본 Behaviors 추가
           	if (GetMoveClass() != MoveType.Stationary) Behaviors.Add(new BasicMoveBehavior(GetMoveClass()));
            if (GetEquipment() != null) Behaviors.Add(new BasicAttackBehavior(GetEquipment()));
        }

		public void Update()
		{
			InitializeBehaviors();
		}

		public abstract MoveType GetMoveClass();
		public abstract Weapon GetEquipment();
		public Status GetFinalStatus()
		{
			return LiveStat.Buffed();
		}
		public Status GetOriginStatus()
		{
			return LiveStat.Stat;
		}

		public float GetEffectMagnitude(BuffEffect effect)
		{
			return LiveStat.GetEffectMagnitude(effect);
		}
		public bool HasBuff(int code)
        {
			return LiveStat.HasBuff(code);
        }

		// 이 유닛의 특정 스탯을 파싱하는 함수
		public int ParseStat(StatType statType, bool isOrigin=false)
		{
			if (statType == StatType.CurrentHp) return CurrentHp();
			Status sourceStat;
			if (isOrigin) sourceStat = GetOriginStatus();
			else sourceStat = GetFinalStatus();
			return BattleManager.Instance.GetParticularStat(sourceStat, statType);
        }
		// 이 유닛이 어떤 공격을 맞았을 때의 피해를 계산하는 함수
		// 치유량도 계산한다
		public int CalculateDamage(int damage, DamageType damageType)
		{
			int finalDamage = damage;
			float residence = 1;
			switch (damageType)
			{
				case DamageType.Physical:
					finalDamage -= GetFinalStatus().Defense;
					residence = 1 - GetEffectMagnitude(BuffEffect.PhysicalResidence) / 100;
					break;
				case DamageType.Magical:
					finalDamage -= GetFinalStatus().MagicDefense;
					residence = 1 - GetEffectMagnitude(BuffEffect.MagicalResidence) / 100;
					break;
			}
			finalDamage = (int)(finalDamage * residence);
			if (finalDamage == 0 && damage > 0) finalDamage = 1; // 시스템적 편의 + 변수를 위한 최소 1뎀
			return finalDamage;
		}

		public bool Alive()
		{
			return LiveStat.CurrentHp >= 0;
		}
		
		public int CurrentHp()
        {
			return LiveStat.CurrentHp;
        }
		public override string ToString()
		{
			// 이동 타입의 첫 글자를 반환.
			string initial = GetMoveClass().ToString().Substring(0, 1);
			if (GetMoveClass() == MoveType.Knight) initial = "N";
			if (Team != Teams.Players) initial = initial.ToLower();
			return initial;
		}
	}
}