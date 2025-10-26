using System.Collections.Generic;

namespace GfEngine.Battles.Patterns
{
    public class PatternSet : GameObject
    {
        public HashSet<Pattern> Patterns { get; set; }

        public PatternSet(HashSet<Pattern> patterns)
        {
            Patterns = patterns;
        }
        public PatternSet(PatternSet parent)
        {
            Patterns = new HashSet<Pattern>(parent.Patterns);
        }
        public void ExtendLengths(int amount)
        {
            foreach(Pattern iter in Patterns)
            {
                if (iter is VectorPattern vector && vector.Length > 0)
                {
                    vector.Length += amount;
                    if(vector.Length < 0) vector.Length = 0; // 길이 감소는 0까지. 그 밑은 무한 길이이다.
                }
            }
        }

    }
}