using Game.Server.Logic.Maps;
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
        public string ObjectType => BuildingTypes.SiliconQuarry;

        public string Description => "Silicon quarry";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirement(ResourceResourceTypes.Minerals);

        public IGameObjectFactory GameObjectFactory => new SiliconQuarryFactory();

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Steel, 45));
    }
}