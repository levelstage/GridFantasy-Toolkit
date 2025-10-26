using System;
namespace GfEngine
{
    // 이름과 코드가 존재하는 게임상의 모든 객체의 조상.
    // GfStudio에서 항목으로 존재하는 객체면 다 이 객체를 상속받는다.
    // GameObject가 아닌 것 예: PatternSet, Pattern, Modifier
    public abstract class GameObject : IEquatable<GameObject>
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public GameObject() { }
        public GameObject(GameObject parent)
        {
            Code = parent.Code;
            Name = parent.Name;
        }
        public bool Equals(GameObject other)
        {
            if (other == null) return false;
            if (GetType() != other.GetType()) return false; // 서로 다른 Type끼리 비교할경우 false.
            return Code == other.Code;
        }
        public override bool Equals(object obj)
        {
            if (obj is not GameObject other) return false;
            return Equals(other);
        }
        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}