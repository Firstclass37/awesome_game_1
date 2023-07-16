using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.IronMine.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.IronMine
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IronMineFactory _ironMineFactory;
        private readonly IArea2x2Getter _area2x2Getter;

        public Metadata(IronMineFactory ironMineFactory, IArea2x2Getter area2x2Getter)
        {
            _ironMineFactory = ironMineFactory;
            _area2x2Getter = area2x2Getter;
        }

        public string ObjectType => BuildingTypes.IronMine;

        public string Description => "Iron mine";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirement(ResourceResourceTypes.IronOre);

        public IGameObjectFactory GameObjectFactory => _ironMineFactory;

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Steel, 45));
    }
}