using GfEngine.Battles.Patterns;
using GfEngine.Core.Conditions;
using GfEngine.Models.Classes;

namespace GfEngine.Models.Actors
{
    // 문장. 즉 이동 패턴과 코스트에 대한 정보임.
    // 사실상 이동의 RuledPatternSet을 만들기 위한 설계도이다.
    public class Crest // ==MoveClass
    {
        public int MoveSet { get; set; }
        public int Penetration { get; set; }
        public ICondition ObstructedBy { get; set; }
        public ICondition Accessible { get; set; }
        public int BaseApCost { get; set; }
    }
}