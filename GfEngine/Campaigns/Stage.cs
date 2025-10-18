using GfEngine.Campaigns.Dialogues;
using GfEngine.Battles;

namespace GfEngine.Campaigns
{
    // CampaignEvent -> Stage 로 이름 변경!
    public class Stage
    {
        public string StageId { get; set; } // EventId -> StageId
        public Conversation OpeningConversation { get; set; }
        public Wave Battle { get; set; }
        public Conversation ClosingConversation { get; set; }
    }
}