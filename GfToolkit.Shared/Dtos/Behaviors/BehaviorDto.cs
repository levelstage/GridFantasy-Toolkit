namespace GfToolkit.Shared.Dtos.Behaviors
{
    public abstract class BehaviorDto
	{
        public int Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public BasicPatternType Scope { get; set; }
		public HashSet<BehaviorTag> Tags { get; set; }
		public HashSet<Relation> Accessible { get; set; }
		public int ApCost { get; set; }
		public string Type { get; set; }

		public BehaviorDto()
		{
			Code = 0;
			Name = "";
			Description = "";
			Scope = BasicPatternType.King;
			Tags = new HashSet<BehaviorTag>();
			Accessible = new HashSet<Relation> { Relation.Enemy, Relation.Neutral };
			ApCost = 0;
		}
		public BehaviorDto(BehaviorDto parent)
        {
			Code = parent.Code;
			Name = parent.Name;
			Description = parent.Description;
			Scope = parent.Scope;
			Tags = parent.Tags;
			Accessible = parent.Accessible;
			ApCost = parent.ApCost;
        }
    }
}