using System.Collections.Generic;
namespace GfToolkit.Shared.Battles
{
	public class PatternSet
	{
		public List<Pattern> Patterns;
		public int Penetration;

		public PatternSet(List<Pattern> patterns)
		{
			Patterns = patterns;
			Penetration = 0;
		}

		public PatternSet(List<Pattern> patterns, int penetration)
		{
			Patterns = patterns;
			Penetration = penetration;
		}

		public List<BehaviorTarget> TargetSearcher(Square startSquare, Square[,] map, List<TeamType> accessible)
		{
			List<BehaviorTarget> possibleActions = new List<BehaviorTarget>();
			int startX = startSquare.X;
			int startY = startSquare.Y;
			int sizeX = map.GetLength(1);
			int sizeY = map.GetLength(0);

			// --- 지역 함수: 특정 좌표를 검사하고 리스트에 추가하는 중복 로직 ---
			// 이 함수는 bool 값을 반환합니다: 벡터 탐색을 계속해야 하면 true, 멈춰야 하면 false
			bool ProcessSquare(int x, int y)
			{
				// 1. 맵 경계 검사
				if (x < 0 || x >= sizeX || y < 0 || y >= sizeY)
				{
					return false; // 맵 벗어나면 탐색 중지
				}

				Unit targetUnit = map[y, x].Occupant;
				TeamType targetTeam = (targetUnit != null) ? GameData.GetTeamType(startSquare.Occupant.Team, targetUnit.Team) : TeamType.Air; // Air: 빈 칸을 의미

				// 2. Behavior의 규칙에 따라 이 칸이 유효한 타겟인지 확인
				if (accessible.Contains(targetTeam))
				{
					possibleActions.Add(new BehaviorTarget(x, y, TargetType.Accessible));
				}
				else
				{
					possibleActions.Add(new BehaviorTarget(x, y, TargetType.Unaccessible));
				}

				return true; // 빈 칸이면 계속 탐색
			}


			// --- 메인 로직 ---
			foreach (Pattern p in Patterns)
			{
				// Coordinate 패턴 처리
				// Vector 패턴 처리
				if (p is VectorPattern)
				{
					int currentX = startX;
					int currentY = startY;
					int penetration = Penetration;
					VectorPattern vp = p as VectorPattern;
					int length = vp.Length;

					if (vp.IsJumpVector) // 점프 벡터 처리
					{
						bool penetrated = false;
						while (length -- > 0 || vp.Length == -1)
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
                    else // 통상 벡터 처리
                    {
						while (length -- > 0 || vp.Length == -1)
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

					
				}
				// Pvector 패턴 처리
				
				else ProcessSquare(startX + p.X, startY + p.Y); // 좌표 패턴 처리
			}
			return possibleActions;
		}

	}
}