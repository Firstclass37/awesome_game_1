using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.Home.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.Home
{
    internal class Metadata : IGameObjectMetadata
    {
        public string ObjectType => BuildingTypes.Home;

        public string Description => "Super home";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new HomeFactory();

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Money, 100));
    }
}