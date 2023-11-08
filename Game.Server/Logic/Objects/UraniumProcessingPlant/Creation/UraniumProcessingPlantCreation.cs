using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.UraniumProcessingPlant.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.UraniumProcessingPlant.Creation
{
    internal class UraniumProcessingPlantCreation : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area, int player)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.UraniumProcessingPlant, player)
                .AddArea(root, area)
                .AddInteraction<UraniumProcessingPlantInteraction>()
                .Build();
        }
    }
}