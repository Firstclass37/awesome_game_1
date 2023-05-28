using My_awesome_character.Core.Constatns;

namespace My_awesome_character.Core.Game.Buildings.Build
{
    internal interface IBuildingFactoryProvider
    {
        IBuildingFactory GetFor(BuildingTypes buildingType);
    }
}