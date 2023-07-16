using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.Storage.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Storage.Creation
{
    internal class StorageFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.Storage)
                .AddArea(root, area)
                .AddInteraction<StorageInteraction>()
                .Build();
        }
    }
}