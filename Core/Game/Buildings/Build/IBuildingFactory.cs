using My_awesome_character.Core.Game.Models;
using My_awesome_character.Entities;

namespace My_awesome_character.Core.Game.Buildings.Build
{
    internal interface IBuildingFactory
    {
        Building Create(MapCell targetCell, IAreaCalculator areaCalculator);
    }
}