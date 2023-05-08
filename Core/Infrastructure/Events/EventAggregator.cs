using My_awesome_character.Core.Infrastructure.Extentions;
using System;
using System.Collections.Concurrent;

namespace My_awesome_character.Core.Infrastructure.Events
{
    public class EventAggregator : IEventAggregator
    {
        private static readonly ConcurrentDictionary<string, object> _events = new ConcurrentDictionary<string, object>();

        public TEvent GetEvent<TEvent>() where TEvent : EventBase, new()
        {
            var eventName = typeof(TEvent).GetFriendlyName();
            if (_events.TryGetValue(eventName, out var eventInstance))
                return (TEvent)eventInstance;

            _events.TryAdd(eventName, new TEvent());
            return (TEvent)_events[eventName];
        }
    }  
}