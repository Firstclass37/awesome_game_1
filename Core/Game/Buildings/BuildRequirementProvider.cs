using My_awesome_character.Core.Constatns;
using System;
using System.Collections.Generic;

namespace My_awesome_character.Core.Game.Buildings
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
            return _requirements.ContainsKey(buildingType) ? _requirements[buildingType] : throw new ArgumentOutOfRangeException($"no build requirements for type {buildingType}");
        }
    }
}