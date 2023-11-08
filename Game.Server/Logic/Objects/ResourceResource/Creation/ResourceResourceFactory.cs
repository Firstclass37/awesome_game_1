using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.ResourceResource.Creation
{
    internal class ResourceResourceFactory : IGameObjectFactory
    {
        private readonly string _resourceResourceType;

        public ResourceResourceFactory(string resourceResourceType)
        {
            _resourceResourceType = resourceResourceType;
        }

        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area, int player)
        {
            return new GameObjectAggregatorBuilder(_resourceResourceType, player)
                .AddArea(root, area)
                .Build();
        }
    }
}