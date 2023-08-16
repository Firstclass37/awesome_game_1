using Game.Server.Logic.Maps;
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
        public string ObjectType => BuildingTypes.IronMine;

        public string Description => "Iron mine";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirement(ResourceResourceTypes.IronOre);

        public IGameObjectFactory GameObjectFactory => new IronMineFactory();

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Steel, 45));
    }
}