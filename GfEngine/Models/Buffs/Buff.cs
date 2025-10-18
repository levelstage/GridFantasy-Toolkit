using System.Collections.Generic;
using GfToolkit.Shared;

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
	}
}