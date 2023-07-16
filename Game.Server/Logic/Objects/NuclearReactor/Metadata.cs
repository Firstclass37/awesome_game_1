using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.NuclearReactor.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.NuclearReactor
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IArea2x2Getter _area2X2Getter;
        private readonly NuclearReactorFactory _nuclearReactorFactory;

        public Metadata(IArea2x2Getter area2X2Getter, NuclearReactorFactory nuclearReactorFactory)
        {
            _area2X2Getter = area2X2Getter;
            _nuclearReactorFactory = nuclearReactorFactory;
        }

        public string ObjectType => BuildingTypes.NuclearReactor;

        public string Description => "Nuclear reactor";

        public IAreaGetter AreaGetter => _area2X2Getter;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => _nuclearReactorFactory;

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 100),
            new ResourceChunk(ResourceType.Coal, 30));
    }
}