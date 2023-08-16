using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.Block
{
    internal class Metadata : IGameObjectMetadata
    {
        public string ObjectType => BuildingTypes.Block;

        public string Description => "Blocking block";

        public AreaSize Size => AreaSize.Area1x1;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new SimpleGameObjectFactory(ObjectType, true);

        public Price BasePrice => Price.Free;
    }
}