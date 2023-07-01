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

        public Coordiante RootCell => GameObject.Area.First(p => p.IsRoot).Coordiante;

        public IReadOnlyCollection<Coordiante> Cells => GameObject.Area.Select(a => a.Coordiante).ToArray();

        public IReadOnlyDictionary<Direction, Coordiante> Tracking => GameObject.GetAttributeValue<Dictionary<Direction, Coordiante>>(AttributeType.TrafficLightTrackingCells);

        public IReadOnlyDictionary<Direction, int> Sizes => GameObject.GetAttributeValue<Dictionary<Direction, int>>(AttributeType.TrafficLightSidesCapacity);

        public IReadOnlyDictionary<Direction, int> CurrentValues => GameObject.GetAttributeValue<Dictionary<Direction, int>>(AttributeType.TrafficLightSidesValues);
    }

    internal class GameObjectProperty<T>
    {
        public GameObjectProperty(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}