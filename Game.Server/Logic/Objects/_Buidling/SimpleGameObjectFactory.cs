using Game.Server.DataBuilding;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Buidling
{
    internal class SimpleGameObjectFactory : IGameObjectFactory
    {
        private readonly string _type;
        private readonly bool _blocking;

        public SimpleGameObjectFactory(string buildingType, bool blocking)
        {
            _type = buildingType;
            _blocking = blocking;
        }

        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area, int player)
        {
            return new GameObjectAggregatorBuilder(_type, player)
                .AddArea(root, area, _blocking)
                .Build();
        }
    }
}