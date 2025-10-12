using GfEngine.Core;
using GfEngine.Models.Buffs;
using System.Collections.Generic;
namespace GfEngine.Models.Statuses
{
	public class LiveStatus
	{
		public Status Stat { get; set; }
		public int CurrentHp { get; set; }
		public List<BuffSet> Buffs { get; set; }

		// 생성자: '기본 스탯(Status)'을 바탕으로 '현재 상태'를 생성.
		public LiveStatus(Status baseStat)
		{
			this.Stat = baseStat;
			// 현재 체력은 최대 체력과 같게 초기화.
			this.CurrentHp = baseStat.MaxHp;
			// 빈 버프 리스트 생성.
			this.Buffs = new List<BuffSet>();
		}

		public Status Buffed()
		{
			Status buffed_status = new Status(this.Stat);
			foreach (BuffSet bSet in Buffs)
			{
				foreach (Buff iter in bSet.Effects)
				{
					switch (iter.Type)
					{
						case BuffType.MaxHpBoost: buffed_status.MaxHp += (int)iter.Magnitude; break;
						case BuffType.DefenseBoost: buffed_status.Defense += (int)iter.Magnitude; break;
						case BuffType.MagicDefenseBoost: buffed_status.MagicDefense += (int)iter.Magnitude; break;
						case BuffType.AttackBoost: buffed_status.Attack += (int)iter.Magnitude; break;
						case BuffType.MagicAttackBoost: buffed_status.MagicAttack += (int)iter.Magnitude; break;
						case BuffType.AgilityBoost: buffed_status.Agility += (int)iter.Magnitude; break;
					}
				}

			}
			return buffed_status;
		}

		public int ChangeHp(int amount)
		{
			// Buffed()를 호출해서 버프가 적용된 현재의 MaxHp를 가져옵니다.
			int currentMaxHp = this.Buffed().MaxHp;
			int newHp = this.CurrentHp + amount;
			int dealt = amount;

			if (newHp < 0)
			{
				dealt = -this.CurrentHp;
				this.CurrentHp = 0;
			}
			else if (newHp > currentMaxHp)
			{
				dealt = currentMaxHp - this.CurrentHp;
				this.CurrentHp = currentMaxHp; // 버프 적용된 MaxHp 기준으로 제한
			}
			else this.CurrentHp = newHp;
			return dealt;
		}
	}
}