using Game.Server.Logic.Maps;
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
        public string ObjectType => BuildingTypes.WaterPump;

        public string Description => "Water pump";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirement(ResourceResourceTypes.Groundwater);

        public IGameObjectFactory GameObjectFactory => new WaterPumpFactory();

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Steel, 10));
    }
}