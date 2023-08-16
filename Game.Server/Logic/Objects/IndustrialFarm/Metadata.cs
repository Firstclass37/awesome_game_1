using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.IndustrialFarm.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.IndustrialFarm
{
    internal class Metadata : IGameObjectMetadata
    {
        public string ObjectType => BuildingTypes.IndustrialFarm;

        public string Description => "Industrial farm";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new IndustrialFarmFactory();

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 25),
            new ResourceChunk(ResourceType.Glass, 10));
    }
}