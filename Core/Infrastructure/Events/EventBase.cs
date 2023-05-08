using System.Collections.Concurrent;
using System;

namespace My_awesome_character.Core.Infrastructure.Events
{
    public abstract class EventBase { }

    public abstract class PubSubEvent : EventBase
    {
        private readonly ConcurrentDictionary<Action, bool> actions = new ConcurrentDictionary<Action, bool>();

        public void Subscribe(Action data)
        {
            if (actions.ContainsKey(data))
                throw new ArgumentException("action was already subscribed");

            actions.TryAdd(data, true);
        }

        public void Publish()
        {
            foreach (var action in actions.Keys)
                action();
        }

        public void Unsubscribe(Action data)
        {
            actions.TryRemove(data, out var _);
        }
    }

    public abstract class PubSubEvent<TData> : EventBase
    {
        private ConcurrentDictionary<Action<TData>, bool> actions = new ConcurrentDictionary<Action<TData>, bool>();

        public void Subscribe(Action<TData> data)
        {
            if (actions.ContainsKey(data))
                throw new ArgumentException("action was already subscribed");

            actions.TryAdd(data, true);
        }

        public void Publish(TData data)
        {
            foreach (var action in actions.Keys)
                action(data);
        }

        public void Unsubscribe(Action<TData> data)
        {
            actions.TryRemove(data, out var _);
        }
    }
}