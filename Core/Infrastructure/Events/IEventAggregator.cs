namespace My_awesome_character.Core.Infrastructure.Events
{
    public interface IEventAggregator
    {
        TEvent GetEvent<TEvent>() where TEvent : EventBase, new();
    }
}