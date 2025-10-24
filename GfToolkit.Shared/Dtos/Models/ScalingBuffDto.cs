namespace GfToolkit.Shared.Dtos.Models
{
	public class ScalingModifierDto : ModifierDto
	{
		public float ScaleFactor { get; set; } // 스케일링 계수
        public StatType TargetStat { get; set; } // 이 버프가 참고할 스탯의 종류
		public bool SelfSourced { get; set; }
	}
}