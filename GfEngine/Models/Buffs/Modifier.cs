using System.Collections.Generic;
using GfToolkit.Shared;
using GfEngine.Battles.Units;

namespace GfEngine.Models.Buffs
{
	public class Modifier
	{
		// 버프를 이루는 가장 기본적인 속성. 모든 Buff는 Modifier들의 집합이다.
		public BuffEffect Effect { get; set; } // 이 버프의 효과
		public float Magnitude { get; set; } // 버프용 수치. 버프 자체는 데이터 덩어리이고, 각 말단 함수들에서 버프의 타입과 magnitude를 참조하여 버프가 반영된 수치를 계산.
		public Unit SourceUnit { get; set; } // 버프를 부여한 유닛이 있다면. 없으면 null이다.
		public Modifier() { }

		public Modifier(BuffEffect effect)
		{
			Effect = effect;
			Magnitude = 0;
		}

		public Modifier(Modifier buff)
		{
			Effect = buff.Effect;
			Magnitude = buff.Magnitude;
		}

		public Modifier(BuffEffect effect, float magnitude)
		{
			Effect = effect;
			Magnitude = magnitude;
		}
	}
}