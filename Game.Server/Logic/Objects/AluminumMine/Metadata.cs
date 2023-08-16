using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.AluminumMine.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.AluminumMine
{
    internal class Metadata : IGameObjectMetadata
    {
        public string ObjectType => BuildingTypes.AluminumMine;

        public string Description => "Aluminum Mine";

        public Price BasePrice => new Price(new ResourceChunk(ResourceType.Steel, 45));

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirement(ResourceResourceTypes.Bauxite);

        public IGameObjectFactory GameObjectFactory => new AluminumMineFactory();
    }
}