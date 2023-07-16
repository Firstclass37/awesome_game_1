using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.SiliconQuarry.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.SiliconQuarry
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IArea2x2Getter _area2x2Getter;
        private readonly SiliconQuarryFactory _siliconQuarryFactory;

        public Metadata(IArea2x2Getter area2x2Getter, SiliconQuarryFactory siliconQuarryFactory)
        {
            _area2x2Getter = area2x2Getter;
            _siliconQuarryFactory = siliconQuarryFactory;
        }

        public string ObjectType => BuildingTypes.SiliconQuarry;

        public string Description => "Silicon quarry";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirement(ResourceResourceTypes.Minerals);

        public IGameObjectFactory GameObjectFactory => _siliconQuarryFactory;

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Steel, 45));
    }
}