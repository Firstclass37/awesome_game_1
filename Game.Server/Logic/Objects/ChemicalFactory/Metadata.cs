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
        private readonly IArea2x2Getter _area2X2Getter;
        private readonly ChemicalFactoryFactory _chemicalFactoryFactory;

        public Metadata(IArea2x2Getter area2X2Getter, ChemicalFactoryFactory chemicalFactoryFactory)
        {
            _area2X2Getter = area2X2Getter;
            _chemicalFactoryFactory = chemicalFactoryFactory;
        }

        public string ObjectType => BuildingTypes.ChemicalFactory;

        public string Description => "Chemical Factory";

        public IAreaGetter AreaGetter => _area2X2Getter;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirement(BuildingTypes.Ground);

        public IGameObjectFactory GameObjectFactory => _chemicalFactoryFactory;

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 30),
            new ResourceChunk(ResourceType.Coal, 20));
    }
}