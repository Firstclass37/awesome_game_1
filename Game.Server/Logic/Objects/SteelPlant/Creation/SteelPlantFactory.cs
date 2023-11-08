using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.SteelPlant.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.SteelPlant.Creation
{
    internal class SteelPlantFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area, int player)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.SteelPlant, player)
                .AddArea(root, area)
                .AddInteraction<SteelPlantInteraction>()
                .Build();
        }
    }
}