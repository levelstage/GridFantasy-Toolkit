using System.Collections.Generic;
using GfEngine.Battles.Squares;
using GfEngine.Battles.Units;

namespace GfEngine.Battles
{
    public class BattleContext
    {
        // 필수존
        public Square OriginSquare { get; }
        public Square TargetSquare { get; }
        public Square[,] Map { get; }
        public Unit OriginUnit { get; }
        public Unit TargetUnit { get; }

        public BattleContext(Square originSquare, Square targetSquare = null, Square[,] map = null,
            Unit explicitOriginUnit = null, Unit explicitTargetUnit = null)
        {
            OriginSquare = originSquare;
            TargetSquare = targetSquare;
            Map = map;
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