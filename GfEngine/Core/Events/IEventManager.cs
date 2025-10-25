using System;

namespace GfEngine.Core.Events
{
    public interface IEventManager
    {
        void Subscribe(Enum eventType, IEventListener listener);
        void BroadcastEvent(Enum eventType, IContext context);
    }
}