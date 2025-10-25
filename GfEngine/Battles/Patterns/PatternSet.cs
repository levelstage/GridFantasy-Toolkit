using System.Collections.Generic;

namespace GfEngine.Battles.Patterns
{
    public class PatternSet : GameObject
    {
        public List<Pattern> Patterns { get; set; }

        public PatternSet(List<Pattern> patterns)
        {
			Patterns = patterns;
        }

    }
}