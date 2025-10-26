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

    }
}