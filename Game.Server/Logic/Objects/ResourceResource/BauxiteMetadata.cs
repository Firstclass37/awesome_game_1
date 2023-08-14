using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.ResourceResource
{
    internal class BauxiteMetadata : IGameObjectMetadata
    {
        private readonly IArea1x1Getter _area1;

        public BauxiteMetadata(IArea1x1Getter area1)
        {
            _area1 = area1;
        }

        public string ObjectType => ResourceResourceTypes.Bauxite;

        public string Description => "Bauxite";

        public IAreaGetter AreaGetter => _area1;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new SimpleGameObjectFactory(ObjectType, true);

        public Price BasePrice => Price.Free;
    }
}