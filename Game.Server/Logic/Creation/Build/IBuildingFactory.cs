using Game.Server.Logic.Building;
using Game.Server.Models.Buildings;
using Game.Server.Models.Maps;

namespace My_awesome_character.Core.Game.Buildings.Build
{
    internal interface IBuildingFactory
    {
        IBuilding Create(Coordiante targetCell, IAreaCalculator areaCalculator, IMap map);
    }
}