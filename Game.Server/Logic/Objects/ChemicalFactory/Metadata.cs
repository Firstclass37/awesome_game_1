using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.ChemicalFactory.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.ChemicalFactory
{
    internal class Metadata : IGameObjectMetadata
    {
        public string ObjectType => BuildingTypes.ChemicalFactory;

        public string Description => "Chemical Factory";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirement(BuildingTypes.Ground);

        public IGameObjectFactory GameObjectFactory => new ChemicalFactoryFactory();

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 30),
            new ResourceChunk(ResourceType.Coal, 20));
    }
}