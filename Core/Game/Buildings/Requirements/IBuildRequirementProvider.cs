using My_awesome_character.Core.Constatns;

namespace My_awesome_character.Core.Game.Buildings.Requirements
{
    public interface IBuildRequirementProvider
    {
        IBuildRequirement GetRequirementFor(BuildingTypes buildingType);
    }
}