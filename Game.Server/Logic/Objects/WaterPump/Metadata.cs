using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.WaterPump.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.WaterPump
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IArea2x2Getter _area2x2Getter;
        private readonly WaterPumpFactory _waterPumpFactory;

        public Metadata(IArea2x2Getter area2x2Getter, WaterPumpFactory waterPumpFactory)
        {
            _area2x2Getter = area2x2Getter;
            _waterPumpFactory = waterPumpFactory;
        }

        public string ObjectType => BuildingTypes.WaterPump;

        public string Description => "Water pump";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirement(ResourceResourceTypes.Groundwater);

        public IGameObjectFactory GameObjectFactory => _waterPumpFactory;

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Steel, 10));
    }
}