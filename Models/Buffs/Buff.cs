using System.Collections.Generic;

namespace GfEngine.Models.Buffs
{
	public class Buff
	{
		//가장 기본적인 속성
		public BuffType Type { get; set; } // 버프의 타입.
		public float Magnitude { get; set; } // 버프용 수치. 버프 자체는 데이터 덩어리이고, 각 말단 함수들에서 버프의 타입과 magnitude를 참조하여 버프가 반영된 수치를 계산.

		public Buff() { }

		public Buff(BuffType type)
		{
			this.Type = type;
			this.Magnitude = 0;
		}

		public Buff(Buff buff)
		{
			this.Type = buff.Type;
			this.Magnitude = buff.Magnitude;
		}

		public Buff(BuffType type, float magnitude)
		{
			this.Type = type;
			this.Magnitude = magnitude;
		}

		public static float netBuffMagnitude(BuffType type, List<BuffSet> buffList)
		{
			// 특정 종류 버프의 magnitude를 모두 합해서 return하는 함수.
			float res = 0;
			foreach (BuffSet bSet in buffList)
			{
				foreach (Buff iter in bSet.Effects)
				{
					if (iter.Type == type) res += iter.Magnitude;
				}
			}
			return res;
		}
	}
}