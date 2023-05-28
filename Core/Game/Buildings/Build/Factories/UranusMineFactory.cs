using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Events.Character;
using My_awesome_character.Core.Game.Events.Resource;
using My_awesome_character.Core.Game.Models;
using My_awesome_character.Core.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Game.Buildings.Build.Factories
{
    internal class UranusMineFactory : IBuildingFactory
    {
        private readonly IEventAggregator _eventAggregator;

        public UranusMineFactory(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public Building Create(MapCell targetCell, IAreaCalculator areaCalculator)
        {
            var rootCell = new MapCell(targetCell.X, targetCell.Y, MapCellType.Building);

            var building = new Building();
            building.Id = new Random().Next(1, 1000000000);
            building.Cells = CreateBuildingArea(targetCell, areaCalculator).ToArray();
            building.RootCell = rootCell;
            building.BuildingType = BuildingTypes.MineUranus;
            building.InteractionAction = CreateInteraction();

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

        private IInteractionAction CreateInteraction()
        {
            return new CommonInteractionAction(c =>
            {
                _eventAggregator.GetEvent<GameEvent<TakeDamageCharacterEvent>>().Publish(new TakeDamageCharacterEvent { CharacterId = c.Id, Damage = 1000 });
                _eventAggregator.GetEvent<GameEvent<ResourceIncreaseEvent>>().Publish(new ResourceIncreaseEvent { Amount = 2, ResourceTypeId = ResourceType.Uranus });
            });
        }
    }
}