namespace GfToolkit.Shared.Campaign.Dialogues
{
    public class DialogueLine
    {
        public ActorCode Speaker { get; set; }
        public string Text { get; set; }

        public PortraitEmotion Emotion { get; set; }

        public DialogueLine()
        {
            // 기본값은 'Normal'로 설정해두면 편해.
            this.Emotion = PortraitEmotion.Normal;
        }
    }
}