using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using My_awesome_character.Core.Game.Buildings.Build;

namespace Game.Server.Logic.Building.Build.Factories
{
    internal class RoadFactory : IBuildingFactory
    {
        public IBuilding Create(Coordiante targetCell, IAreaCalculator areaCalculator, IMap map)
        {
            var rootCell = targetCell;

            var building = new CommonBuilding();
            building.Id = new Random().Next(1, 1000000000);
            building.Cells = new Coordiante[] { targetCell };
            building.RootCell = rootCell;
            building.BuildingType = BuildingTypes.Road;
            return building;
        }
    }
}