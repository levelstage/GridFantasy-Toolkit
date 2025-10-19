namespace GfToolkit.Shared.Dtos.Models
{
	public class ScalingBuffDto : BuffDto
	{
		public float ScaleFactor { get; set; } // 스케일링 계수
        public StatType TargetStat { get; set; } // 이 버프가 참고할 스탯의 종류
	}
}