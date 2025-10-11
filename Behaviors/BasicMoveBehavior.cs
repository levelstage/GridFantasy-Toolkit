namespace GfEngine.Behaviors
{
    public class BasicMoveBehavior : Behavior
{
		public BasicMoveBehavior(MoveType moveType) // 이동 behavior 생성자
		{
			this.Name = GameData.Text.Get(GameData.Text.Key.Command_Move);
			this.Scope = GameData.MovePatterns[moveType];
			this.ApCost = GameData.MoveApCosts[moveType];
			this.Accessible = new List<TeamType> { TeamType.Air };
			// "폰"처럼 이동에 특별한 처리가 필요한 경우 tag를 따로 부여해 준다.
			this.Tags = new List<BehaviorTag>();
			if(GameData.SpecialMoves.ContainsKey(moveType)){
				this.Tags.Add(GameData.SpecialMoves[moveType]);
			}

		}
		public override string Excute(Square origin, Square target)
		{
            //폰에 대한 예외처리. 폰의 이동방식을 전진한 폰의 이동방식으로 바꿔준다.
            if(this.Tags.Contains(BehaviorTag.PawnFirstDown)){
                this.Scope = GameData.MovePatterns[MoveType.Pawn_Down_Advanced];
                this.Tags.Remove(BehaviorTag.PawnFirstDown);
            }
            if(this.Tags.Contains(BehaviorTag.PawnFirstUp)){
                this.Scope = GameData.MovePatterns[MoveType.Pawn_Up_Advanced];
                this.Tags.Remove(BehaviorTag.PawnFirstUp);
            }
            // 여기가 메인 로직. 말 그대로 이동을 처리함.
            target.PlaceUnit(origin.Occupant);
            origin.ClearUnit();
            return "";
		}
}

}