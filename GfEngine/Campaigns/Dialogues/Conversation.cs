using System.Collections.Generic;

namespace GfEngine.Campaigns.Dialogues
{
    public class Conversation
    {
        // 이 대화의 고유 ID
        public string Id { get; set; }

        // 이 대화를 구성하는 모든 대화 라인(대사 하나하나)의 목록
        public List<DialogueLine> Lines { get; set; }

        public Conversation()
        {
            Lines = new List<DialogueLine>();
        }
    }
}