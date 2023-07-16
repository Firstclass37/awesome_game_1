using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.CoalMine.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.CoalMine
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IArea2x2Getter _area2x2Getter;
        private readonly CoalMineFactory _coalMineFactory;

        public Metadata(IArea2x2Getter area2x2Getter, CoalMineFactory coalMineFactory)
        {
            _area2x2Getter = area2x2Getter;
            _coalMineFactory = coalMineFactory;
        }

        public string ObjectType => BuildingTypes.CoalMine;

        public string Description => "Coal Mine";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirement(ResourceResourceTypes.Coke);

        public IGameObjectFactory GameObjectFactory => _coalMineFactory;

        public Price BasePrice => new Price(new ResourceChunk(ResourceType.Steel, 45));
    }
}