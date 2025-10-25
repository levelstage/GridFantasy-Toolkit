using GfEngine.Battles.Patterns;
using GfEngine.Battles.Commands;
using GfEngine.Core.Conditions;
using GfEngine.Core;
using GfEngine.Battles.Conditions;
using GfEngine.Logics;
using GfToolkit.Shared;

namespace GfEngine.Battles.Behaviors
{
	public abstract class Behavior : GameObject
	{
		public string Description { get; set; }
		public RuledPatternSet Scope { get; set; }
		public int ApCost { get; set; }
		public Behavior()
		{
			Name = "Undefined Behavior";
			Description = "No description.";
			ApCost = 1; // 기본 행동력 소모량
		}
		
		public abstract Command Execute(BattleContext context);
	}
}