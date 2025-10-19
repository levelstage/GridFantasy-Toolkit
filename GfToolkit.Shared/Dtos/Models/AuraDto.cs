namespace GfToolkit.Shared.Dtos.Models
{
    public class AuraDto : BuffDto
    {
        public BasicPatternType AuraScope { get; set; } // 오라의 범위
        public int AuraBuffCode { get; set; } // 오라 범위 내의 대상에게 부여할 버프셋
        public List<TeamType> AuraTargets { get; set; } // 오라의 대상 (아군, 적군 등)
        // 동적 범위 지정 관련 속성들
        public bool UseMovePattern { get; set; } = false; // 유닛의 현재 이동 범위를 사용할지 여부
        public bool UseAttackPattern { get; set; } = false; // 유닛의 현재 공격 범위를 사용할지 여부
    }
}