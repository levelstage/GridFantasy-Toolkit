using System.Collections.Generic;

namespace GfEngine.Battles.Patterns
{
    public abstract class PatternSystem : GameObject
    {
        public List<(PatternSet, int)> PatternInfos { get; set; }

        public int Index { get; set; }

        public PatternSystem()
        {
            
        }

        public PatternSet SelectedPatternSet()
        {
            return PatternInfos[Index].Item1;
        }
        public int SelectedApCost()
        {
            return PatternInfos[Index].Item2;
        }
        public abstract int GetNextPattern();
    }
}