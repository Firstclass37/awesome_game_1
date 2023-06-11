using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Buildings.Build.Interactions;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Models;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using System;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Game.Buildings.Build.Factories
{
    internal class PowerStationFactory : IBuildingFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public PowerStationFactory(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public Building Create(MapCell targetCell, IAreaCalculator areaCalculator, IMap map)
        {
            var rootCell = new MapCell(targetCell.X, targetCell.Y, MapCellType.Building);

            var building = new Building();
            building.Id = new Random().Next(1, 1000000000);
            building.Cells = CreateBuildingArea(targetCell, areaCalculator).ToArray();
            building.RootCell = rootCell;
            building.BuildingType = BuildingTypes.MineUranus;
            building.InteractionAction = new PowerStatitionInteraction(_eventAggregator, _sceneAccessor);

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
    }
}