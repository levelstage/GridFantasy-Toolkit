using GfEngine.Battles;
using System.Collections.Generic;
using GfToolkit.Shared;

namespace GfEngine.Behaviors
{
	public class BasicMoveBehavior : Behavior
	{
		public BasicMoveBehavior(MoveType moveType) // 이동 behavior 생성자
		{
			Name = GameData.Text.Get(GameData.Text.Key.Command_Move);
			Scope = GameData.MovePatterns[moveType];
			ApCost = GameData.MoveApCosts[moveType];
			Accessible = new HashSet<TeamType> { TeamType.Air };
			// "폰"처럼 이동에 특별한 처리가 필요한 경우 tag를 따로 부여해 준다.
			Tags = new HashSet<BehaviorTag>();
			if (GameData.SpecialMoves.ContainsKey(moveType))
			{
				Tags.Add(GameData.SpecialMoves[moveType]);
			}

		}
		public override string Execute(Square origin, Square target, Square[,] map)
		{
			//폰에 대한 예외처리. 폰의 이동방식을 전진한 폰의 이동방식으로 바꿔준다.
			if (Tags.Contains(BehaviorTag.PawnFirstDown))
			{
				Scope = GameData.MovePatterns[MoveType.Pawn_Down_Advanced];
				Tags.Remove(BehaviorTag.PawnFirstDown);
			}
			if (Tags.Contains(BehaviorTag.PawnFirstUp))
			{
				Scope = GameData.MovePatterns[MoveType.Pawn_Up_Advanced];
				Tags.Remove(BehaviorTag.PawnFirstUp);
			}
			// 여기가 메인 로직. 말 그대로 이동을 처리함.
			target.PlaceUnit(origin.Occupant);
			origin.ClearUnit();
			return "";
		}
	}

}