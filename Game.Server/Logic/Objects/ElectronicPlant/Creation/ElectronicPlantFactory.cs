using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.ElectronicPlant.Interactions;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.ElectronicPlant.Creation
{
    internal class ElectronicPlantFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.ElectronicPlant)
                .AddArea(root, area)
                .AddInteraction<ElectronicPlantInteraction>()
                .Build();
        }
    }
}