using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.SolarBattery.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.SolarBattery
{
    internal class Metadata : IGameObjectMetadata
    {
        public string ObjectType => BuildingTypes.SolarBattery;

        public string Description => "Solar battery";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new SolarBatteryFactory();

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Aluminum, 20), new ResourceChunk(ResourceType.Silicon, 100));
    }
}