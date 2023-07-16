using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.ElectronicPlant.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.ElectronicPlant
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly ElectronicPlantFactory _electronicPlantFactory;
        private readonly IArea2x2Getter _area2x2Getter;

        public Metadata(ElectronicPlantFactory electronicPlantFactory, IArea2x2Getter area2x2Getter)
        {
            _electronicPlantFactory = electronicPlantFactory;
            _area2x2Getter = area2x2Getter;
        }

        public string ObjectType => BuildingTypes.ElectronicPlant;

        public string Description => "Electronic Plant";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => _electronicPlantFactory;

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 30),
            new ResourceChunk(ResourceType.Aluminum, 20));
    }
}