using Game.Server.Models.Buildings;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Building.Build.Factories
{
    internal interface IPowerStationFactory
    {
        IBuilding Create(Coordiante targetCell, IAreaCalculator areaCalculator);
    }
}