namespace GfToolkit.Shared.Core
{
	public class UnitAction
	{
		public int X { get; set; }
		public int Y { get; set; }
		public ActionType Type { get; set; }
		public UnitAction(int x, int y, ActionType type)
		{
			X = x;
			Y = y;
			Type = type;
		}
	}
}