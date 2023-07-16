using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.MoistureTrap.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.MoistureTrap.Creation
{
    internal class MoistureTrapFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.MoistureTrap)
                .AddArea(root, area)
                .AddInteraction<MoistureTrapInteraction>()
                .Build();
        }
    }
}