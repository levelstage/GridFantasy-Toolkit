namespace GfToolkit.Shared.Dtos.Models
{
    public class BuffDto
    {
        public BuffEffect Type { get; set; } // 이 버프가 어떤 효과를 가졌는지.
		public float Magnitude { get; set; } // 버프용 수치. 버프 자체는 데이터 덩어리이고, 각 말단 함수들에서 버프의 타입과 magnitude를 참조하여 버프가 반영된 수치를 계산.
    }
}