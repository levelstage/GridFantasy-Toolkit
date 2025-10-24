using System.Collections.Generic;
using GfEngine.Battles.Squares;
using GfEngine.Battles.Units;

namespace GfEngine.Battles
{
    public class BattleContext
    {
        public Square Origin { get; set; }
        public Square Target { get; set; }
        public Square[,] Map { get; set; }

        public BattleContext(Square origin, Square target = null, Square[,] map = null)
        {
            Origin = origin;
            Target = target;
            Map = map;
        }
    }
}