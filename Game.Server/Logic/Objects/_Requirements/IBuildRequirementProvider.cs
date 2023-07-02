
namespace Game.Server.Logic.Objects._Requirements
{
    internal interface IBuildRequirementProvider
    {
        ICreationRequirement GetRequirementFor(string buildingType);
    }
}