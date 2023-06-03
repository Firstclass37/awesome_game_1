namespace My_awesome_character.Core.Infrastructure.Events.Extentions
{
    public static class EventAggregatorExtentions
    {
        public static void PublishGameEvent<TEvent>(this IEventAggregator eventAggregator, TEvent @event) =>
            eventAggregator.GetEvent<GameEvent<TEvent>>().Publish(@event);

    }
}