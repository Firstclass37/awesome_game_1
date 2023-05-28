using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Models;
using System;

namespace My_awesome_character.Core.Game.Buildings.Build.Factories
{
    internal class RoadFactory : IBuildingFactory
    {
        public Building Create(MapCell targetCell, IAreaCalculator areaCalculator)
        {
            var rootCell = new MapCell(targetCell.X, targetCell.Y, MapCellType.Road, MapCellTags.Trap);

            var building = new Building();
            building.Id = new Random().Next(1, 1000000000);
            building.Cells = new MapCell[] { targetCell };
            building.RootCell = rootCell;
            building.BuildingType = BuildingTypes.Road;
            return building;
        }
    }
}