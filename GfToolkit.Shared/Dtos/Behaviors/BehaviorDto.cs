namespace GfToolkit.Shared.Dtos.Behaviors
{
    public abstract class BehaviorDto
	{
        public int Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public BasicPatternType Scope { get; set; }
		public List<BehaviorTag> Tags { get; set; }
		public List<TeamType> Accessible { get; set; }
		public int ApCost { get; set; }
		public string Type { get; set; }

		public BehaviorDto()
        {
			Code = 0;
			Name = "";
			Description = "";
			Scope = BasicPatternType.King;
			Tags = new();
			Accessible = new List<TeamType> { TeamType.Enemy, TeamType.Neutral };
			ApCost = 0;
        }
    }
}