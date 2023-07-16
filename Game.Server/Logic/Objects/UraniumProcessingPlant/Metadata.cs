using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.UraniumProcessingPlant.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.UraniumProcessingPlant
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IArea2x2Getter _area2x2Getter;
        private readonly UraniumProcessingPlantCreation _uraniumProcessingPlantCreation;

        public Metadata(IArea2x2Getter area2x2Getter, UraniumProcessingPlantCreation uraniumProcessingPlantCreation)
        {
            _area2x2Getter = area2x2Getter;
            _uraniumProcessingPlantCreation = uraniumProcessingPlantCreation;
        }

        public string ObjectType => BuildingTypes.UraniumProcessingPlant;

        public string Description => "Uranium processing plant";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => _uraniumProcessingPlantCreation;

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 30), 
            new ResourceChunk(ResourceType.Chemicals, 20));
    }
}