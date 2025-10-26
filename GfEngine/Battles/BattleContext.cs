using System.Collections.Generic;
using GfEngine.Battles.Squares;
using GfEngine.Battles.Units;
using GfEngine.Core;

namespace GfEngine.Battles
{
    public class BattleContext : IContext
    {
        // 필드는 get-only로 유지하여 불변성을 확보합니다.
        public Wave WaveData { get; }
        public Square OriginSquare { get; }
        public Square TargetSquare { get; }
        public Unit OriginUnit { get; }
        public Unit TargetUnit { get; }

        public BattleContext(
            BattleContext baseContext = null,
            Wave waveData = null,
            Square originSquare = null,
            Square targetSquare = null,
            Unit originUnit = null,
            Unit targetUnit = null)
        {
            // 1. BaseContext 복사 (최초 시작 시에는 모두 null)
            if (baseContext != null)
            {
                // 기본 Context의 모든 필드를 복사합니다. (이후 인수로 덮어씀)
                WaveData = baseContext.WaveData;
                OriginSquare = baseContext.OriginSquare;
                TargetSquare = baseContext.TargetSquare;
                OriginUnit = baseContext.OriginUnit;
                TargetUnit = baseContext.TargetUnit;
            }
            // baseContext가 null일 경우, 인수를 사용하거나 null로 유지합니다.
            
            // --- 2. WaveData 및 인수로 받은 Square/Unit 덮어쓰기 (우선순위 1) ---
            
            // WaveData는 명시적 인수가 있으면 덮어씁니다.
            if (waveData != null) WaveData = waveData;
            
            // Square 정보 덮어쓰기
            if (originSquare != null) OriginSquare = originSquare;
            if (targetSquare != null) TargetSquare = targetSquare;

            // Unit 정보 덮어쓰기
            if (originUnit != null) OriginUnit = originUnit;
            if (targetUnit != null) TargetUnit = targetUnit;

            // --- 3. Unit <-> Square 상호 보완 (우선순위 2) ---

            // OriginUnit 보완: Unit이 없는데 Square가 채워져 있다면 Unit을 채웁니다.
            if (OriginUnit == null && OriginSquare != null && OriginSquare.Occupant != null)
                OriginUnit = OriginSquare.Occupant;

            // OriginSquare 보완: Square가 없는데 Unit이 채워져 있다면 Square를 채웁니다.
            if (OriginSquare == null && OriginUnit != null && OriginUnit.CurrentSquare != null)
                OriginSquare = OriginUnit.CurrentSquare;

            // TargetUnit 보완: Unit이 없는데 Square가 채워져 있다면 Unit을 채웁니다.
            if (TargetUnit == null && TargetSquare != null && TargetSquare.Occupant != null)
                TargetUnit = TargetSquare.Occupant;

            // TargetSquare 보완: Square가 없는데 Unit이 채워져 있다면 Square를 채웁니다.
            if (TargetSquare == null && TargetUnit != null && TargetUnit.CurrentSquare != null)
                TargetSquare = TargetUnit.CurrentSquare;
        }
    }
}