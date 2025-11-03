using GfEngine.Battles.Patterns;
using GfEngine.Battles.Commands;

namespace GfEngine.Battles.Behaviors
{
	public abstract class Behavior : GameObject
	{
		public RuledPatternSet Scope { get; set; }
		public int ApCost { get; set; }
		public Behavior() { }
		public abstract Command Execute(BattleContext context);
	}
}