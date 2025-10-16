using GfToolkit.Shared.Core;
using GfToolkit.Shared.Battles;
using System.Collections.Generic;

namespace GfToolkit.Shared.Behaviors
{
	public abstract class Behavior
	{
		public string Name { get; set; }
		public PatternSet Scope { get; set; }
		public List<BehaviorTag> Tags { get; set; }
		public List<TeamType> Accessible { get; set; }
		public int ApCost { get; set; }
		public Behavior()
		{
			Name = "Undefined Behavior";
			Tags = new List<BehaviorTag>();
			Accessible = new List<TeamType> { TeamType.Enemy }; // 기본값: 적군만 공격 가능
			ApCost = 1; // 기본 행동력 소모량
		}
		
		public abstract string Execute(Square origin, Square target, Square[,] map);
	}
}