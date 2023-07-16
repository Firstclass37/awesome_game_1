using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.WaterPump.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.WaterPump.Creation
{
    internal class WaterPumpFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.WaterPump)
                .AddArea(root, area)
                .AddInteraction<WaterPumpInteraction>()
                .Build();
        }
    }
}