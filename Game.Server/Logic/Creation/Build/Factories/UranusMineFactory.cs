using Game.Server.Events.Core;
using Game.Server.Events.Core.Extentions;
using Game.Server.Events.List.Character;
using Game.Server.Events.List.Homes;
using Game.Server.Logic.Resources;
using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using My_awesome_character.Core.Game.Buildings.Build;

namespace Game.Server.Logic.Building.Build.Factories
{
    internal class UranusMineFactory : IBuildingFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IResourceManager _resourceManager;

        public UranusMineFactory(IEventAggregator eventAggregator, IResourceManager resourceManager)
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
            building.InteractionAction = CreateInteraction(building.Id);

            return building;
        }

        private IInteractionAction CreateInteraction(int buildingId)
        {
            return new CommonInteractionAction(c =>
            {
                _eventAggregator.PublishGameEvent(new TakeDamageCharacterEvent { CharacterId = c.Id, Damage = 1000 });
                _eventAggregator.PublishGameEvent(new BuildingDelayedActionEvent
                {
                    BuidlingId = buildingId,
                    DelaySec = 5,
                    Event = () => _resourceManager.Increase(ResourceType.Uranus, 2)
                });
            });
        }
    }
}