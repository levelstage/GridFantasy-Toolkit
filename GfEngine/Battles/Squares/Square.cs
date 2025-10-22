using GfToolkit.Shared;
using GfEngine.Battles.Units;
using System.Collections.Generic;

namespace GfEngine.Battles.Squares
{
    public class Square
    {
        public TerrainType Terrain { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Unit Occupant { get; set; } // 이 변수가 null인지 아닌지로 점유 상태를 판단.
        public List<GroundEffect> GroundEffects { get; set; } = new List<GroundEffect>(); // 이 칸에 걸려있는 지형 효과들.

        public Square(int x, int y, TerrainType type = TerrainType.Plains)
        {
            X = x;
            Y = y;
            Terrain = type;
            Occupant = null; // 처음에는 비어있으므로 null로 초기화
        }

        public void PlaceUnit(Unit occupant)
        {
            Occupant = occupant;
        }
        public void ClearUnit()
        {
            Occupant = null;
        }

        public void Update()
        {
            if (Occupant != null) Occupant.Update();
        }

        public void TurnStart()
        {
            if (Occupant != null) Occupant.TurnStart();
        }

        public void TurnOver()
        {
            if (Occupant != null) Occupant.TurnOver();
        }

        public override string ToString()
        {
            string open_text = "[";
            string content = " ";

            switch (this.Terrain)
            {
                case TerrainType.Swamp: open_text = "("; break;
                case TerrainType.Water: open_text = "~"; break;
            }

            // Occupant가 null이 아니면 (유닛이 있으면)
            if (Occupant != null)
            {
                content = Occupant.ToString(); // Unit의 ToString()을 호출
            }
            return open_text + content;
        }
    }
}