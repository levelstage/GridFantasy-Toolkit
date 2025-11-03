using GfToolkit.Shared;
using GfEngine.Battles.Squares;
using GfEngine.Battles.Conditions;
using GfEngine.Core.Conditions;
using GfEngine.Logics;
using GfEngine.Battles.Units;
using System.Collections.Generic;
using System;

namespace GfEngine.Battles.Patterns
{
    public class RuledPatternSet
    {
        public PatternSet PatternSetToUse { get; set; }
        public ICondition Accessible { get; set; }
		public ICondition ObstructedBy { get; set; }
		public int Penetration { get; set; }

        public RuledPatternSet(PatternSet patternSet, ICondition accessible = null,
            ICondition obstructedBy = null, int penetration = 0)
        {
            PatternSetToUse = patternSet;
            if (accessible == null) Accessible = new NoCondition();
            else Accessible = accessible;
            if (obstructedBy == null) ObstructedBy = new BattleComparingCondition(
            BattleManager.Instance.BattleFormulaParser,
            "TS.IsOccupied()",
            "1",
             ComparisonOperator.Equal
            );
            else ObstructedBy = obstructedBy;
            Penetration = penetration;
        }
        /// <summary>
        /// 이 PatternSet을 기반으로 유효한 대상 리스트를 검색합니다.
        /// </summary>
        /// <param name="startSquare">시작 타일 (OriginSquare)</param>
        /// <param name="wave">현재 전투 맵 상태 (Wave)</param>
        /// <param name="accessible">대상이 유효한지(e.g., 적군인지) 검사하는 ICondition</param>
        public List<BehaviorTarget> GetValidTargets(Square startSquare, Wave wave)
        {
            List<BehaviorTarget> possibleActions = new List<BehaviorTarget>();
            Square[,] map = wave.Map;
            int startX = startSquare.X;
            int startY = startSquare.Y;
            int sizeX = map.GetLength(1);
            int sizeY = map.GetLength(0);

            // --- 지역 함수: 특정 좌표를 검사하고 리스트에 추가하는 중복 로직 ---
            // [수정됨] BattleContext를 인자로 받아 중복 생성을 방지합니다.
            void ProcessSquare(BattleContext context, int x, int y)
            {
                // Behavior의 규칙에 따라 이 칸이 유효한 타겟인지 확인
                if (Accessible.IsMet(context))
                {
                    possibleActions.Add(new BehaviorTarget(x, y, TargetType.Accessible));
                }
                else
                {
                    possibleActions.Add(new BehaviorTarget(x, y, TargetType.Unaccessible));
                }
            }

            // --- 메인 로직 ---
            foreach (Pattern p in PatternSetToUse.Patterns)
            {
                // C# 7.0+ 패턴 매칭 사용 권장
                if (p is VectorPattern vp)
                {
                    int currentX = startX;
                    int currentY = startY;
                    int currentPen = Penetration;
                    int length = vp.Length;
                    bool penetrated = false; // 점프 벡터용 플래그

                    while (length-- > 0 || vp.Length == -1) // 무한 길이(-1) 지원
                    {
                        currentX += p.X;
                        currentY += p.Y;

                        // [FIX 1] 맵 경계 검사를 *가장 먼저* 수행
                        if (currentX < 0 || currentX >= sizeX || currentY < 0 || currentY >= sizeY)
                        {
                            break; // 맵 범위를 벗어나면 벡터 탐색 중지
                        }

                        // [FIX 2] BattleContext를 *한 번만* 생성 (이제 크래시로부터 안전함)
                        BattleContext battleContext = new BattleContext(waveData: wave, originSquare: startSquare, targetSquare: map[currentY, currentX]);

                        // 점프 벡터 처리
                        if (vp.IsJumpVector)
                        {
                            if (!penetrated) // 아직 장애물을 찾지 못함 (점프 전)
                            {
                                // 장애물 조건 검사
                                if (ObstructedBy.IsMet(battleContext))
                                {
                                    currentPen--;
                                    penetrated = true; // 장애물을 찾았음 (점프 발생)
                                    if (currentPen < 0) break; // 관통 불가
                                }
                                // 점프 전에는 ProcessSquare를 호출하지 않음 (타겟 추가 안 함)
                            }
                            else // 장애물을 찾음 (점프 완료), 이제부터 타겟 처리
                            {
                                // 타겟 유효성 검사 (Accessible/Unaccessible 추가)
                                ProcessSquare(battleContext, currentX, currentY);

                                // 점프 후에도 추가 장애물이 있으면 관통력 소모
                                if (ObstructedBy.IsMet(battleContext))
                                {
                                    currentPen--;
                                    if (currentPen < 0) break; // 추가 관통 불가
                                }
                            }
                        }
                        else // 통상 벡터 처리
                        {
                            // 타겟 유효성 검사 (Accessible/Unaccessible 추가)
                            ProcessSquare(battleContext, currentX, currentY);

                            // 장애물 조건 검사
                            if (ObstructedBy.IsMet(battleContext))
                            {
                                currentPen--;
                                if (currentPen < 0) break; // 관통 불가
                            }
                        }
                    }
                }
                else // 좌표(Coordinate) 패턴 처리
                {
                    int x = startX + p.X;
                    int y = startY + p.Y;

                    // [FIX 1] 맵 경계 검사
                    if (x < 0 || x >= sizeX || y < 0 || y >= sizeY)
                    {
                        continue; // 이 좌표는 무시하고 다음 패턴으로
                    }

                    // [FIX 2] BattleContext 생성 및 ProcessSquare 호출
                    BattleContext battleContext = new BattleContext(waveData: wave, originSquare: startSquare, targetSquare: map[y, x]);
                    ProcessSquare(battleContext, x, y);
                }
            }
            return possibleActions;
        }
        public bool IsAccessible(Square startSquare, Square targetSquare, Wave wave)
        {
            // null 체크
            if (startSquare == null || targetSquare == null || wave == null) return false;
            Square[,] map = wave.Map;
            BattleContext battleContext = new BattleContext(waveData: wave, originSquare: startSquare, targetSquare: targetSquare);
            if (!Accessible.IsMet(battleContext)) // 애초에 대상이 될 수 없는 경우 false
            {
                return false;
            }
            foreach (Pattern p in PatternSetToUse.Patterns)
            {
                int dx = targetSquare.X - startSquare.X;
                int dy = targetSquare.Y - startSquare.Y;
                if (p is VectorPattern vp) // 벡터패턴 처리
                {
                    // 0벡터가 들어올 일은 거의 없지만, 그래도 예외 처리를 해주자.
                    if (vp.X == 0 && vp.Y == 0)
                    {
                        if (dx == 0 && dy == 0) return true;
                        continue;
                    }
                    // 0벡터가 아니라면, 올바른 방향에 있는지 확인.
                    if (dx * vp.Y != dy * vp.X || dx * vp.X < 0 || dy * vp.Y < 0) continue;
                    // 그 다음 길이를 체크한다.      
                    if (Math.Abs(dx) > Math.Abs(vp.X * vp.Length) || Math.Abs(dy) > Math.Abs(vp.Y * vp.Length)) continue;
                    // 마지막으로, 사이에 있는 장애물 개수를 체크한다.
                    int obstruction = 0;
                    for (int i = 1; i < (vp.X != 0 ? dx / vp.X : dy / vp.Y); i++)
                    {
                        int curX = startSquare.X + i * vp.X;
                        int curY = startSquare.Y + i * vp.Y;
                        battleContext = new BattleContext(waveData: wave, originSquare: startSquare, targetSquare: map[curY, curX]);
                        if (ObstructedBy.IsMet(battleContext)) obstruction++;
                    }
                    // 점프벡터라면 최소 하나 이상 뛰어넘어야 함.
                    if (vp.IsJumpVector && obstruction == 0) continue;
                    // 마지막 조건인 장애물 수 <= 관통력까지 통과하여야 true를 return.
                    if (obstruction <= Penetration) return true;
                }
                else // 일반적인 좌표패턴의 경우.
                {
                    // 복잡한 계산 없이 좌표 처리만 하면 됨.
                    if (dx == p.X && dy == p.Y) return true;
                }
            }
            return false;
        }
    }
}