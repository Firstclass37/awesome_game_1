using Game.Server.Models.Maps;

namespace Game.Server.Events.List.Movement
{
    public class GameObjectPositionChangedEvent
    {
        public Guid GameObjectId { get; init; }

        public string GameObjectType { get; init; }

        public Coordiante PreviousPostion { get; init; }

        public Coordiante NewPosition { get; init; }
    }

    public class GameObjectPositionChangingEvent
    {
        public Guid GameObjectId { get; init; }

        public string GameObjectType { get; init; }

        public Coordiante CurrentPosition { get; init; }

        public Coordiante TargetPosition { get; init; }

        public double Speed { get; init; }
    }
}