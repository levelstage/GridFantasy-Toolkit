namespace GfEngine.Models.Stages
{
    public class Square
    {
        public TerrainType Terrain { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Unit Occupant { get; set; } // 이 변수가 null인지 아닌지로 점유 상태를 판단.

        public Square(int x, int y, TerrainType type = TerrainType.Plains)
        {
            this.X = x;
            this.Y = y;
            this.Terrain = type;
            this.Occupant = null; // 처음에는 비어있으므로 null로 초기화
        }

        public void PlaceUnit(Unit occupant)
        {
            this.Occupant = occupant;
        }
        public void ClearUnit()
        {
            this.Occupant = null;
        }

        public void Update()
        {
            this.Occupant.Update();
        }

        public void TurnStart()
        {
            this.Occupant.TurnStart();
        }

        public void TurnOver()
        {
            this.Occupant.TurnOver();
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
            if (this.Occupant != null)
            {
                content = this.Occupant.ToString(); // Unit의 ToString()을 호출
            }
            return open_text + content;
        }
    }
}