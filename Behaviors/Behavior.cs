using GfEngine.Core;
using GfEngine.Stages;
using System.Collections.Generic;

namespace GfEngine.Behaviors
{
	public abstract class Behavior
	{
		public string Name { get; set; }
		public PatternSet Scope { get; set; }
		public List<BehaviorTag> Tags { get; set; }
		public List<TeamType> Accessible { get; set; }
		public int ApCost { get; set; }
		public List<UnitAction> ActionSearcher(Square startSquare, Square[,] map, Behavior B)
		{
			List<UnitAction> possibleActions = new List<UnitAction>();
			int startX = startSquare.X;
			int startY = startSquare.Y;
			int sizeX = map.GetLength(1);
			int sizeY = map.GetLength(0);

			// --- 지역 함수: 특정 좌표를 검사하고 리스트에 추가하는 중복 로직 ---
			// 이 함수는 bool 값을 반환합니다: 벡터 탐색을 계속해야 하면 true, 멈춰야 하면 false
			bool ProcessSquare(int x, int y)
			{
				// 1. 맵 경계 검사 (버그 수정: >= 0)
				if (x < 0 || x >= sizeX || y < 0 || y >= sizeY)
				{
					return false; // 맵 벗어나면 탐색 중지
				}

				Unit targetUnit = map[y, x].Occupant;
				TeamType targetTeam = (targetUnit != null) ? GameData.GetTeamType(startSquare.Occupant.Team, targetUnit.Team) : TeamType.Air; // Air: 빈 칸을 의미

				// 2. Behavior의 규칙에 따라 이 칸이 유효한 타겟인지 확인
				if (B.Accessible.Contains(targetTeam))
				{
					possibleActions.Add(new UnitAction(x, y, ActionType.Accessible));
				}
				else
				{
					possibleActions.Add(new UnitAction(x, y, ActionType.Unaccessible));
				}

				// 3. 벡터 탐색 계속 여부 결정
				if (targetTeam != TeamType.Air) // 칸에 유닛이 있다면
				{
					return B.Scope.Penetration > 0; // 관통력이 남아있으면 계속, 없으면 중지
				}

				return true; // 빈 칸이면 계속 탐색
			}


			// --- 메인 로직 ---
			foreach (Pattern p in B.Scope.Patterns)
			{
				if (p.Type == PatternType.Coordinate)
				{
					ProcessSquare(startX + p.X, startY + p.Y);
				}
				else if (p.Type == PatternType.Vector)
				{
					int currentX = startX;
					int currentY = startY;
					int penetration = B.Scope.Penetration;

					while (true)
					{
						currentX += p.X;
						currentY += p.Y;

						if (!ProcessSquare(currentX, currentY)) // 지역 함수 호출, false 반환 시 루프 중단
						{
							break;
						}

						// ProcessSquare 내부에서 유닛 존재 여부를 판단했으므로,
						// 여기서 관통력 감소 로직을 처리해야 함
						if (map[currentY, currentX].Occupant != null)
						{
							penetration--;
							if (penetration < 0) break;
						}
					}
				}
				else if (p.Type == PatternType.Pvector)
				{
					int currentX = startX;
					int currentY = startY;
					int penetration = B.Scope.Penetration + 1;
					bool penetrated = false;

					while (true)
					{
						currentX += p.X;
						currentY += p.Y;
						if (!penetrated)
						{
							//아무것도 뛰어넘지 않았을 경우. 뛰어넘을 대상을 탐색.
							if (currentX < 0 || currentX >= sizeX || currentY < 0 || currentY >= sizeY)
							{
								break; // 맵 범위를 벗어나면 탐색 끝.
							}
						}
						else if (!ProcessSquare(currentX, currentY)) // 뛰어넘었다면, 대상을 탐색해준다.
						{
							break;
						}

						// ProcessSquare 내부에서 유닛 존재 여부를 판단했으므로,
						// 여기서 관통력 감소 로직을 처리해야 함
						if (map[currentY, currentX].Occupant != null)
						{
							penetration--;
							penetrated = true;
							if (penetration < 0) break;
						}

					}
				}
			}
			return possibleActions;
		}
		public abstract string Excute(Square origin, Square target);
	}
}