using GfEngine.Models.Buffs;
using GfToolkit.Shared;
using System.Collections.Generic;
namespace GfEngine.Models.Statuses
{
	public class LiveStatus
	{
		public Status Stat { get; }
		public int CurrentHp { get; set; }
		public List<Buff> Buffs { get; set; }
		public List<Modifier> EffectiveModifiers { get; set; }

		// 생성자: '기본 스탯(Status)'을 바탕으로 '현재 상태'를 생성.
		public LiveStatus(Status baseStat)
		{
			Stat = baseStat;
			// 현재 체력은 최대 체력과 같게 초기화.
			CurrentHp = baseStat.MaxHp;
			// 빈 버프 리스트 생성.
			Buffs = new List<Buff>();
			EffectiveModifiers = new List<Modifier>();
		}

		public float GetEffectMagnitude(BuffEffect effect)
		{
			float res = 0;
			foreach (Modifier iter in EffectiveModifiers)
			{
				if (iter.Effect == effect) res += iter.Magnitude;
			}
			return res;
		}
		
		public bool HasBuff(int code)
        {
			foreach (Buff buff in Buffs)
			{
				if (buff.Code == code) return true;
			}
			return false;
        }

		public Status Buffed()
		{
			Status buffed_status = new Status(Stat);
			buffed_status.MaxHp += (int)GetEffectMagnitude(BuffEffect.MaxHpBoost);
			buffed_status.Defense += (int)GetEffectMagnitude(BuffEffect.DefenseBoost);
			buffed_status.MagicDefense += (int)GetEffectMagnitude(BuffEffect.MagicDefenseBoost);
			buffed_status.Attack += (int)GetEffectMagnitude(BuffEffect.AttackBoost);
			buffed_status.MagicAttack += (int)GetEffectMagnitude(BuffEffect.MagicAttackBoost);
			buffed_status.Agility += (int)GetEffectMagnitude(BuffEffect.AgilityBoost);
			return buffed_status;
		}

		public int ChangeHp(int amount)
		{
			// Buffed()를 호출해서 버프가 적용된 현재의 MaxHp를 가져옵니다.
			int currentMaxHp = Buffed().MaxHp;
			int newHp = CurrentHp + amount;
			int dealt = amount;

			if (newHp < 0)
			{
				dealt = -CurrentHp;
				CurrentHp = 0;
			}
			else if (newHp > currentMaxHp)
			{
				dealt = currentMaxHp - CurrentHp;
				CurrentHp = currentMaxHp; // 버프 적용된 MaxHp 기준으로 제한
			}
			else CurrentHp = newHp;
			return dealt;
		}
	}
}