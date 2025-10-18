using GfEngine.Core;
using GfEngine.Battles;
using System.Collections.Generic;

namespace GfEngine.Behaviors
{
	public abstract class Behavior
	{
		public int Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public PatternSet Scope { get; set; }
		public List<BehaviorTag> Tags { get; set; }
		public List<TeamType> Accessible { get; set; }
		public int ApCost { get; set; }
		public Behavior()
		{
			Name = "Undefined Behavior";
			Description = "No description.";
			Tags = new List<BehaviorTag>();
			Accessible = new List<TeamType> { TeamType.Enemy }; // 기본값: 적군만 공격 가능
			ApCost = 1; // 기본 행동력 소모량
		}
		
		public abstract string Execute(Square origin, Square target, Square[,] map);
	}
}