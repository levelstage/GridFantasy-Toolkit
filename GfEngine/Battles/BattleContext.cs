using System.Collections.Generic;
using GfEngine.Battles.Squares;
using GfEngine.Battles.Units;
using GfEngine.Core;

namespace GfEngine.Battles
{
    public class BattleContext : IContext
    {
        // 필수존
        public Wave WaveData { get; }
        public Square OriginSquare { get; }
        public Square TargetSquare { get; }
        public Unit OriginUnit { get; }
        public Unit TargetUnit { get; }

        public BattleContext(Wave waveData = null, Square originSquare = null, Square targetSquare = null,
            Unit explicitOriginUnit = null, Unit explicitTargetUnit = null)
        {
            OriginSquare = originSquare;
            TargetSquare = targetSquare;
            WaveData = waveData;
            // 굳이 OriginUnit을 넣어주지 않아도 OriginSquare에 유닛이 있다면 그 유닛이 OriginUnit.
            if (originSquare != null && explicitOriginUnit == null && originSquare.Occupant != null)
                OriginUnit = OriginSquare.Occupant;
            else OriginUnit = explicitOriginUnit;
            // 마찬가지.
            if (targetSquare != null && explicitTargetUnit == null && targetSquare.Occupant != null)
                TargetUnit = TargetSquare.Occupant;   
            else TargetUnit = explicitTargetUnit;
        }
    }
}