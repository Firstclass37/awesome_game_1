using Game.Server.Events.Core;
using System;

namespace My_awesome_character.Common.Extentions
{
    internal static class EventAgregatorExtentions
    {
        public static void SubscribeGameEvent<T>(this IEventAggregator eventAggregator, Action<T> action)
            => eventAggregator.GetEvent<GameEvent<T>>().Subscribe(action); 
    }
}
