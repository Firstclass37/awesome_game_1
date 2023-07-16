using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.Storage.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.Storage
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IArea2x2Getter _area2x2Getter;
        private readonly StorageFactory _storageFactory;

        public Metadata(IArea2x2Getter area2x2Getter, StorageFactory storageFactory)
        {
            _area2x2Getter = area2x2Getter;
            _storageFactory = storageFactory;
        }

        public string ObjectType => BuildingTypes.Storage;

        public string Description => "Storage";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => _storageFactory;

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Steel, 10));
    }
}