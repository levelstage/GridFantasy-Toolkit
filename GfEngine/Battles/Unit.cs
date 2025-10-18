using GfEngine.Behaviors;
using GfEngine.Models.Actors;
using GfEngine.Models.Items;
using GfEngine.Models.Statuses;
using GfEngine.Models.Buffs;
using System.Collections.Generic;
using GfEngine.Logics;
namespace GfEngine.Battles
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

		public void TurnStart()
		{

		}

		public void TurnOver()
		{

		}

		public abstract MoveType GetMoveClass();
		public abstract Weapon GetEquipment();

		// 피해를 받는 함수.
		public int TakeDamage(int damage, DamageType damage_type)
		{
			LiveStatus livestat = this.LiveStat;
			int finalDamage = damage;
			float residence = 1;
			switch (damage_type)
			{
				case DamageType.Physical:
					finalDamage -= livestat.Buffed().Defense;
					residence = 1 - BattleManager.NetBuffMagnitude(BuffType.PhysicalResidence, livestat.Buffs) / 100;
					break;
				case DamageType.Magical:
					finalDamage -= livestat.Buffed().MagicDefense;
					residence = 1 - BattleManager.NetBuffMagnitude(BuffType.MagicalResidence, livestat.Buffs) / 100;
					break;

			}
			if (finalDamage < 0) finalDamage = 0;
			finalDamage = (int)(finalDamage * residence);
			return livestat.ChangeHp(-finalDamage);
		}

		public override string ToString()
		{
			// 이동 타입의 첫 글자를 반환.
			string initial = this.GetMoveClass().ToString().Substring(0, 1);
			if (this.GetMoveClass() == MoveType.Knight) initial = "N";
			if (this.Team != Teams.Players) initial = initial.ToLower();
			return initial;
		}
	}
}