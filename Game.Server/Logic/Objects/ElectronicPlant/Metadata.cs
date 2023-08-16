using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.ElectronicPlant.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.ElectronicPlant
{
    internal class Metadata : IGameObjectMetadata
    {
        public string ObjectType => BuildingTypes.ElectronicPlant;

        public string Description => "Electronic Plant";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new ElectronicPlantFactory();

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 30),
            new ResourceChunk(ResourceType.Aluminum, 20));
    }
}