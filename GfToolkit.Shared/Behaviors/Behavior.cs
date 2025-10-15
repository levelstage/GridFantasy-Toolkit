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
		// ActionSearcher: 특정 유닛이 이 Behavior를 사용했을 때 가능한 행동들을 계산
		// startSquare: 행동을 시작하는 유닛의 위치
		// map: 현재 전장의 전체 맵 (2D 배열)
		// B: 이 Behavior 자체 (범위, 관통력, 접근 가능 대상 등 정보 포함)
		// 반환값: 가능한 행동들의 리스트 (BehaviorTarget 객체들)
		
		public abstract string Execute(Square origin, Square target, Square[,] map);
	}
}