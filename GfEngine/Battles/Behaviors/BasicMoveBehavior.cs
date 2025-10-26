using GfEngine.Battles.Squares;
using GfEngine.Battles.Commands.Core;
using GfEngine.Battles.Units;
using GfEngine.Battles.Commands;
using GfEngine.Battles.Patterns;

namespace GfEngine.Battles.Behaviors
{
	public class BasicMoveBehavior : Behavior
	{
		public BasicMoveBehavior(RuledPatternSet scope, int apCost) // 이동 behavior 생성자
		{
			Scope = scope;
			ApCost = apCost;
		}
		private static string GetSquareName(int x, int y, int ySize)
        {
			string res = "";
			//(0, 0) => a(ysize)
			res = ((char)('a' + x)).ToString() + ((char)('0' + ySize - y)).ToString();
			return res;
        }
		public override Command Execute(BattleContext context)
		{
			Unit o = context.OriginUnit;
			Square ts = context.TargetSquare;

			MoveCommand res = new MoveCommand()
			{
				SourceUnit = o,
				TargetSquare = ts,
				MovedSquareDescription = GetSquareName(ts.X, ts.Y, context.WaveData.Map.GetLength(0))
			};
			return res;
		}
	}

}