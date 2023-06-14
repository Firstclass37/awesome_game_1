using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Events;
using My_awesome_character.Core.Game.Events.Character;
using My_awesome_character.Core.Game.Events.Homes;
using My_awesome_character.Core.Game.Events.Resource;
using My_awesome_character.Core.Game.Models;
using My_awesome_character.Core.Game.Resources;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Infrastructure.Events.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Game.Buildings.Build.Factories
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

        public Building Create(MapCell targetCell, IAreaCalculator areaCalculator, IMap map)
        {
            var rootCell = new MapCell(targetCell.X, targetCell.Y, MapCellType.Building);

            var building = new Building();
            building.Id = new Random().Next(1, 1000000000);
            building.Cells = CreateBuildingArea(targetCell, areaCalculator).ToArray();
            building.RootCell = rootCell;
            building.BuildingType = BuildingTypes.MineUranus;
            building.InteractionAction = CreateInteraction(building.Id);

            return building;
        }

        private IEnumerable<MapCell> CreateBuildingArea(MapCell center, IAreaCalculator areaCalculator)
        {
            var area = areaCalculator.Get2x2Area(center);
            foreach (var cell in area)
            {
                var c = cell;
                c.CellType = MapCellType.Building;
                c.Tags = new string[] { MapCellTags.Trap };
                yield return c;
            }
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
                    Event = () => _resourceManager.Increse(ResourceType.Uranus, 2)
                });
            });
        }
    }
}