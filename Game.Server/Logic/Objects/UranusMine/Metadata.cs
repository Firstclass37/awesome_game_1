using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.UranusMine.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.UranusMine
{
    internal class Metadata : IGameObjectMetadata
    {
        public string ObjectType => BuildingTypes.UranusMine;

        public string Description => "Uranus mine";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirementv2(ResourceResourceTypes.Uranium, GroundTypes.Ground);

        public IGameObjectFactory GameObjectFactory => new UranusMineFactory();

        public Price BasePrice => Price.Free;
    }
}