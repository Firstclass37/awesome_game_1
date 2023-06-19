namespace Game.Server.Events.Core.Extentions
{
    internal static class EventAggregatorExtentions
    {
        public static void PublishGameEvent<TEvent>(this IEventAggregator eventAggregator, TEvent @event) =>
            eventAggregator.GetEvent<GameEvent<TEvent>>().Publish(@event);
    }
}