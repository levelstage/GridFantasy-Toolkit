using GfEngine.Battles.Patterns;
using GfEngine.Battles.Squares;
using System.Collections.Generic;
using GfToolkit.Shared;
using GfEngine.Battles.Commands;

namespace GfEngine.Battles.Behaviors
{
	public abstract class Behavior : GameObject
	{
		public string Description { get; set; }
		public PatternSet Scope { get; set; }
		public HashSet<BehaviorTag> Tags { get; set; }
		public HashSet<TeamType> Accessible { get; set; }
		public int ApCost { get; set; }
		public Behavior()
		{
			Name = "Undefined Behavior";
			Description = "No description.";
			Tags = new HashSet<BehaviorTag>();
			Accessible = new HashSet<TeamType> { TeamType.Enemy }; // 기본값: 적군만 공격 가능
			ApCost = 1; // 기본 행동력 소모량
		}
		
		public abstract Command Execute(Square origin, Square target, Square[,] map);
	}
}