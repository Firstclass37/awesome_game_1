using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.Gorund.Creation;
using Game.Server.Logic.Objects.Gorund.Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.Gorund
{
    internal class Metadata : IGameObjectMetadata
    {
        public string ObjectType => GroundTypes.Ground;

        public string Description => "Default ground";

        public Price BasePrice => Price.Free;

        public AreaSize Size => AreaSize.Area1x1;

        public ICreationRequirement CreationRequirement => new GroundRequirement();

        public IGameObjectFactory GameObjectFactory => new GroundFactory();
    }
}