using My_awesome_character.Core.Game.Models;

namespace My_awesome_character.Core.Game.Buildings.Build.Factories
{
    internal interface IPowerStationFactory
    {
        Building Create(MapCell targetCell, IAreaCalculator areaCalculator);
    }
}