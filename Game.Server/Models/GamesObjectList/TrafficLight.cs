using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Models.Buildings
{
    internal class TrafficLight
    {
        public TrafficLight(GameObjectAggregator objectAggregator) 
        {
            GameObject = objectAggregator;
        }

        public GameObjectAggregator GameObject { get; }

        public Guid Id => GameObject.GameObject.Id;
    }

    internal class TrafficLightAttributesTypes
    {
        public const string TrafficLightTrackingCells = nameof(TrafficLightTrackingCells);
        public const string TrafficLightSidesValues = nameof(TrafficLightSidesValues);
        public const string TrafficLightSidesCapacity = nameof(TrafficLightSidesCapacity);
    }

    internal static class TrafficLightAttributes
    {
        public static GameObjectAttribute<Dictionary<Direction, Coordiante>> TrafficLightTrackingCells => new GameObjectAttribute<Dictionary<Direction, Coordiante>>(TrafficLightAttributesTypes.TrafficLightTrackingCells);
        public static GameObjectAttribute<Dictionary<Direction, int>> TrafficLightSidesValues => new GameObjectAttribute<Dictionary<Direction, int>>(TrafficLightAttributesTypes.TrafficLightSidesValues);
        public static GameObjectAttribute<Dictionary<Direction, int>> TrafficLightSidesCapacity => new GameObjectAttribute<Dictionary<Direction, int>>(TrafficLightAttributesTypes.TrafficLightSidesCapacity);
    }
}