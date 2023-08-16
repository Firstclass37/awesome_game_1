using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.NuclearReactor.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.NuclearReactor
{
    internal class Metadata : IGameObjectMetadata
    {
        public string ObjectType => BuildingTypes.NuclearReactor;

        public string Description => "Nuclear reactor";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new NuclearReactorFactory();

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 100),
            new ResourceChunk(ResourceType.Coal, 30));
    }
}