using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.GlassFactory.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.GlassFactory.Creation
{
    internal class GlassFactoryFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.GlassFactory)
                .AddArea(root, area)
                .AddInteraction<GlassFactoryInteraction>()
                .Build();
        }
    }
}