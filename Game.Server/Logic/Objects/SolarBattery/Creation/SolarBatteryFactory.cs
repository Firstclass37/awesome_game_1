using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.SolarBattery.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.SolarBattery.Creation
{
    internal class SolarBatteryFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area, int player)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.SolarBattery, player)
                .AddArea(root, area)
                .AddInteraction<SolarBatteryInteraction>()
                .Build();
        }
    }
}