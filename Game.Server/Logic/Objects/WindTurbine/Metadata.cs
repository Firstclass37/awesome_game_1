using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.WindTurbine.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.WindTurbine
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IArea2x2Getter _area2x2Getter;
        private readonly WindTurbineFactory _windTurbineFactory;

        public Metadata(IArea2x2Getter area2x2Getter, WindTurbineFactory windTurbineFactory)
        {
            _area2x2Getter = area2x2Getter;
            _windTurbineFactory = windTurbineFactory;
        }

        public string ObjectType => BuildingTypes.WindTurbine;

        public string Description => "Wind turbine";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => _windTurbineFactory;

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Aluminum, 20), new ResourceChunk(ResourceType.Coal, 30));
    }
}