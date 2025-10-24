namespace GfToolkit.Shared.Dtos
{
    public abstract class GameDto
    {
        // GfEngine에서 GameObject로 정의된 Object들의 DTO.
        public int Code { get; set; }
        public string Name { get; set; }
    }
}