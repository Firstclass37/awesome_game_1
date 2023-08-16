using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.MoistureTrap.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.MoistureTrap
{
    internal class Metadata : IGameObjectMetadata
    {
        public string ObjectType => BuildingTypes.MoistureTrap;

        public string Description => "Moisture trap";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new MoistureTrapFactory();

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Steel, 10), new ResourceChunk(ResourceType.Glass, 10));
    }
}