using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.GreenhouseFarm.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.GreenhouseFarm.Creation
{
    internal class GreenhouseFarmFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.GreenhouseFarm)
                .AddArea(root, area)
                .AddInteraction<GreenhouseFarmInteraction>()
                .Build();
        }
    }
}