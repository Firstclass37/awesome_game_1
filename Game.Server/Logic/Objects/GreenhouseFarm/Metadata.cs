using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.GreenhouseFarm.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.GreenhouseFarm
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly GreenhouseFarmFactory _greenhouseFarmFactory;
        private readonly IArea2x2Getter _area2x2Getter;

        public Metadata(GreenhouseFarmFactory greenhouseFarmFactory, IArea2x2Getter area2x2Getter)
        {
            _greenhouseFarmFactory = greenhouseFarmFactory;
            _area2x2Getter = area2x2Getter;
        }

        public string ObjectType => BuildingTypes.GreenhouseFarm;

        public string Description => "Greenhouse farm";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => _greenhouseFarmFactory;

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 15),
            new ResourceChunk(ResourceType.Glass, 10));
    }
}
