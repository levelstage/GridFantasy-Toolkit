using GfToolkit.Shared;
namespace GfEngine.Battles.Patterns
{
	public class BehaviorTarget
	{
		public int X { get; set; }
		public int Y { get; set; }
		public TargetType Type { get; set; }
		public BehaviorTarget(int x, int y, TargetType type)
		{
			X = x;
			Y = y;
			Type = type;
		}
	}
}