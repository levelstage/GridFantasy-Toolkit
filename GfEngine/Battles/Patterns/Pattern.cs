using System;

namespace GfEngine.Battles.Patterns
{
    public class Pattern : IEquatable<Pattern>
	{
		public int X { get; set; }
		public int Y { get; set; }

		public bool Equals(Pattern other)
		{
			if (other == null) return false;
			return (X == other.X) && (Y == other.Y);
		}
        public override bool Equals(object obj)
        {
			if (obj is not Pattern other) return false;
			return Equals(other);
        }
        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }
	}
}

