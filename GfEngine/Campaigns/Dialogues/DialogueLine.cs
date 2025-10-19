using GfToolkit.Shared;
namespace GfEngine.Campaigns.Dialogues
{
    public class DialogueLine
    {
        public string Speaker { get; set; }
        public string Text { get; set; }

        public PortraitEmotion Emotion { get; set; }

        public DialogueLine()
        {
            // 기본값은 'Normal'로 설정해두면 편해.
            Emotion = PortraitEmotion.Normal;
        }
    }
}