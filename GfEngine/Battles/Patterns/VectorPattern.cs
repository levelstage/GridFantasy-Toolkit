using System;

namespace GfEngine.Battles.Patterns
{
    public class VectorPattern : Pattern, IEquatable<VectorPattern>
	{
        public int Length { get; set; }
        public bool IsJumpVector { get; set; } // true면 점프 벡터, false면 일반 벡터
        public bool Equals(VectorPattern other)
        {
            if (other == null) return false;
            return base.Equals(other) && (Length == other.Length) && (IsJumpVector == other.IsJumpVector);
        }
        public override bool Equals(object obj)
        {
            if (obj is not VectorPattern other) return false;
            return Equals(other);
        }
        public override int GetHashCode()
        {
            return (X, Y, Length, IsJumpVector).GetHashCode();
        }
	}
}
