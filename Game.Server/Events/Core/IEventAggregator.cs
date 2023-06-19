namespace Game.Server.Events.Core
{
    public interface IEventAggregator
    {
        TEvent GetEvent<TEvent>() where TEvent : EventBase, new();
    }
}