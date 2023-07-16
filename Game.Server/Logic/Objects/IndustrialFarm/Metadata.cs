using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.IndustrialFarm.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.IndustrialFarm
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IndustrialFarmFactory _industrialFarmFactory;
        private readonly IArea2x2Getter _area2x2Getter;

        public Metadata(IndustrialFarmFactory industrialFarmFactory, IArea2x2Getter area2x2Getter)
        {
            _industrialFarmFactory = industrialFarmFactory;
            _area2x2Getter = area2x2Getter;
        }

        public string ObjectType => BuildingTypes.IndustrialFarm;

        public string Description => "Industrial farm";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => _industrialFarmFactory;

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 25),
            new ResourceChunk(ResourceType.Glass, 10));
    }
}