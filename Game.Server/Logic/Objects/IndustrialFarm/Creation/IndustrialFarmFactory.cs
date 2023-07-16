using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.IndustrialFarm.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.IndustrialFarm.Creation
{
    internal class IndustrialFarmFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.IndustrialFarm)
                .AddArea(root, area)
                .AddInteraction<IndustrialFarmInteraction>()
                .Build();
        }
    }
}