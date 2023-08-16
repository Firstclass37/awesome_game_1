using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.ResourceResource
{
    internal class BauxiteMetadata : IGameObjectMetadata
    {
        public string ObjectType => ResourceResourceTypes.Bauxite;

        public string Description => "Bauxite";

        public AreaSize Size => AreaSize.Area1x1;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new SimpleGameObjectFactory(ObjectType, true);

        public Price BasePrice => Price.Free;
    }
}