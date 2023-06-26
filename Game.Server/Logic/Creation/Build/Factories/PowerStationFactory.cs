using Game.Server.Events.Core;
using Game.Server.Logic.Building.Build.Interactions;
using Game.Server.Logic.Resources;
using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using My_awesome_character.Core.Game.Buildings.Build;

namespace Game.Server.Logic.Building.Build.Factories
{
    internal class PowerStationFactory : IBuildingFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IResourceManager _resourceManager;

        public PowerStationFactory(IEventAggregator eventAggregator, IResourceManager resourceManager)
        {
            _eventAggregator = eventAggregator;
            _resourceManager = resourceManager;
        }

        public IBuilding Create(Coordiante targetCell, IAreaCalculator areaCalculator, IMap map)
        {
            var rootCell = targetCell;

            var building = new CommonBuilding();
            building.Id = new Random().Next(1, 1000000000);
            building.Cells = areaCalculator.Get2x2Area(rootCell);
            building.RootCell = rootCell;
            building.BuildingType = BuildingTypes.MineUranus;
            building.InteractionAction = new PowerStatitionInteraction(_eventAggregator, _resourceManager);

            return building;
        }
    }
}