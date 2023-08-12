using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.Block
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IArea1x1Getter _area1x1Getter;

        public Metadata(IArea1x1Getter area1x1Getter)
        {
            _area1x1Getter = area1x1Getter;
        }

        public string ObjectType => BuildingTypes.Block;

        public string Description => "Blocking block";

        public IAreaGetter AreaGetter => _area1x1Getter;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new SimpleGameObjectFactory(ObjectType, true);

        public Price BasePrice => Price.Free;
    }
}