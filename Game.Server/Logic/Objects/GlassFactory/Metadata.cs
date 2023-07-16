using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.GlassFactory.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.GlassFactory
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IArea2x2Getter _area2x2Getter;
        private readonly GlassFactoryFactory _glassFactoryFactory;

        public Metadata(IArea2x2Getter area2x2Getter, GlassFactoryFactory glassFactoryFactory)
        {
            _area2x2Getter = area2x2Getter;
            _glassFactoryFactory = glassFactoryFactory;
        }

        public string ObjectType => BuildingTypes.GlassFactory;

        public string Description => "Glass Factory";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => _glassFactoryFactory;

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 30),
            new ResourceChunk(ResourceType.Silicon, 20));
    }
}
