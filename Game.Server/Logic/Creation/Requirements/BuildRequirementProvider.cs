using Game.Server.Models.Constants;
using My_awesome_character.Core.Game.Buildings.Requirements;

namespace Game.Server.Logic.Building.Requirements
{
    internal class BuildRequirementProvider : IBuildRequirementProvider
    {
        private readonly Dictionary<BuildingTypes, IBuildRequirement> _requirements = new Dictionary<BuildingTypes, IBuildRequirement>
        {
            { BuildingTypes.HomeType1, new HomeBuildRequirement() },
            { BuildingTypes.PowerStation, new HomeBuildRequirement() },
            { BuildingTypes.MineUranus, new UranusMineBuildRequirement() }
        };

        public IBuildRequirement GetRequirementFor(BuildingTypes buildingType)
        {
            return _requirements.ContainsKey(buildingType) ? _requirements[buildingType] : new OnlyGroundRequirement();
        }
    }
}