using System;

namespace GfEngine.Core.Events
{
    /// <summary>
    /// 모든 이벤트 수신자의 계약입니다.
    /// </summary>
    public interface IEventListener
    {
        // eventType의 인자를 Enum으로 받아 BattleEventType이든 CampaignEventType이든 모두 수용합니다.
        void HandleEvent(Enum eventType, IContext context); 
    }
}