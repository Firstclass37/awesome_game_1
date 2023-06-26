using Game.Server.Models.Constants;
using My_awesome_character.Core.Game.Buildings.Build;

namespace Game.Server.Logic.Building.Build
{
    internal interface IBuildingFactoryProvider
    {
        IBuildingFactory GetFor(BuildingTypes buildingType);
    }
}