using System.Collections.Generic;
using System.Net.Sockets;
using GfEngine.Battles;
using GfToolkit.Shared;

namespace GfEngine.Models.Buffs
{
	public class ScalingModifier : Modifier
	{
		public float ScaleFactor { get; set; } // 스케일링 계수
        public StatType TargetStat { get; set; } // 이 버프가 참고할 스탯의 종류
		public bool SelfSourced; // True라면 버프가 걸린 객체, False라면 버프 부여자의 스탯을 참조함.
	}
}