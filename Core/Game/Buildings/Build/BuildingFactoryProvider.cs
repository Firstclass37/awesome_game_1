using Autofac;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Buildings.Build.Factories;
using System;
using System.Collections.Generic;

namespace My_awesome_character.Core.Game.Buildings.Build
{
    internal class BuildingFactoryProvider : IBuildingFactoryProvider
    {
        private readonly Dictionary<BuildingTypes, Type> _factories = new Dictionary<BuildingTypes, Type>
        {
            { BuildingTypes.Road, typeof(RoadFactory) },
            { BuildingTypes.MineUranus, typeof(UranusMineFactory) },
            { BuildingTypes.PowerStation, typeof(PowerStationFactory) }
        };

        private readonly ILifetimeScope _lifetimeScope;

        public BuildingFactoryProvider(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public IBuildingFactory GetFor(BuildingTypes buildingType)
        {
            if (!_factories.ContainsKey(buildingType))
                throw new ArgumentOutOfRangeException($"no build factories for type {buildingType}");

            var type = _factories[buildingType];
            var factory = _lifetimeScope.Resolve(type) as IBuildingFactory;
            return factory ?? throw new ArgumentOutOfRangeException($"factory for {buildingType} was not found");
        }
    }
}