using Game.Server.Logic.Maps;
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
        public string ObjectType => BuildingTypes.GreenhouseFarm;

        public string Description => "Greenhouse farm";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new GreenhouseFarmFactory();

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 15),
            new ResourceChunk(ResourceType.Glass, 10));
    }
}
