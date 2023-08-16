using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.Roads.Createtion;
using Game.Server.Logic.Objects.Roads.Reuirements;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.Roads
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly RoadCreationRequirement _requirement;

        public Metadata(RoadCreationRequirement requirement)
        {
            _requirement = requirement;
        }

        public string ObjectType => BuildingTypes.Road;

        public string Description => "Road";

        public AreaSize Size => AreaSize.Area1x1;

        public ICreationRequirement CreationRequirement => _requirement;

        public IGameObjectFactory GameObjectFactory => new RoadFactory();

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Money, 10));
    }
}