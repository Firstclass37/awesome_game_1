using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.GlassFactory.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.GlassFactory
{
    internal class Metadata : IGameObjectMetadata
    {
        public string ObjectType => BuildingTypes.GlassFactory;

        public string Description => "Glass Factory";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new GlassFactoryFactory();

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 30),
            new ResourceChunk(ResourceType.Silicon, 20));
    }
}
