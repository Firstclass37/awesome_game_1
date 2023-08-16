using Game.Server.Logic.Maps;
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
        public string ObjectType => BuildingTypes.CoalMine;

        public string Description => "Coal Mine";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirement(ResourceResourceTypes.Coke);

        public IGameObjectFactory GameObjectFactory => new CoalMineFactory();

        public Price BasePrice => new Price(new ResourceChunk(ResourceType.Steel, 45));
    }
}