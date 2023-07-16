using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.MoistureTrap.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.MoistureTrap
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IArea2x2Getter _area2x2Getter;
        private readonly MoistureTrapFactory _moistureTrapFactory;

        public Metadata(IArea2x2Getter area2x2Getter, MoistureTrapFactory moistureTrapFactory)
        {
            _area2x2Getter = area2x2Getter;
            _moistureTrapFactory = moistureTrapFactory;
        }

        public string ObjectType => BuildingTypes.MoistureTrap;

        public string Description => "Moisture trap";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => _moistureTrapFactory;

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Steel, 10), new ResourceChunk(ResourceType.Glass, 10));
    }
}