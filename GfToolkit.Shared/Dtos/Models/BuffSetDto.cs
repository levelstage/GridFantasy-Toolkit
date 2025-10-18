namespace GfToolkit.Shared.Dtos.Models
{
    public class BuffSetDto
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; } // 지속 턴 수
        
        // 속성 플래그
        public bool IsBuff { get; set; }
        public bool IsDebuff { get; set; }
        public bool IsRemovable { get; set; }
        public bool IsVisible { get; set; }
        
        // 이 묶음에 포함된 개별 버프 효과들의 리스트
        public List<BuffDto> Effects { get; set; } = new();
    }
}