using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Events.Character;
using My_awesome_character.Core.Game.Events.Resource;
using My_awesome_character.Core.Game.Events;
using My_awesome_character.Core.Game.Models;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.System;
using System;
using System.Collections.Generic;
using System.Linq;
using My_awesome_character.Core.Game.Resources;

namespace My_awesome_character.Core.Game.Buildings.Build.Factories
{
    internal class HomeFactory : IBuildingFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IResourceManager _resourceManager;

        public HomeFactory(IEventAggregator eventAggregator, IResourceManager resourceManager)
        {
            _eventAggregator = eventAggregator;
            _resourceManager = resourceManager;
        }

        public Building Create(MapCell targetCell, IAreaCalculator areaCalculator, IMap map)
        {
            var rootCell = new MapCell(targetCell.X, targetCell.Y, MapCellType.Building);
            var spawnCell = new MapCell(rootCell.X, rootCell.Y + 3, MapCellType.Groud);

            var building = new Building();
            building.Id = new Random().Next(1, 1000000000);
            building.Cells = CreateBuildingArea(targetCell, areaCalculator).ToArray();
            building.RootCell = rootCell;
            building.BuildingType = BuildingTypes.HomeType1;
            building.PeriodicAction = CreateAction(spawnCell, map);
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
                yield return c;
            }
        }

        private IInteractionAction CreateInteraction()
        {
            return new CommonInteractionAction(c =>
            {
                _eventAggregator.GetEvent<GameEvent<TakeDamageCharacterEvent>>().Publish(new TakeDamageCharacterEvent { CharacterId = c.Id, Damage = 1000 });
                _resourceManager.Increse(ResourceType.Uranus, 2);
            });
        }

        private IPeriodicAction CreateAction(MapCell spawnCell, IMap map)
        {
            var @event = new CharacterCreationRequestEvent { InitPosition = spawnCell };
            var action = () =>
            {
                var actualCell = map.GetActualCell(new Coordiante(spawnCell.X, spawnCell.Y));
                if (actualCell.CellType == MapCellType.Road || actualCell.Tags.Contains(MapCellTags.Trap))
                    _eventAggregator.GetEvent<GameEvent<CharacterCreationRequestEvent>>().Publish(@event);
            };
            return new CommonPeriodicAction(action, 5, SystemNode.GameTime);
        }
    }
}