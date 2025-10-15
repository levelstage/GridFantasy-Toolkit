namespace GfToolkit.Shared.Battles
{
    public class VectorPattern : Pattern
	{
        public int Length { get; set; }
        public bool IsJumpVector { get; set; } // true면 점프 벡터, false면 일반 벡터
	}
}
