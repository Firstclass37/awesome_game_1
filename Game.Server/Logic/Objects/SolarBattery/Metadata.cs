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
        private readonly IArea2x2Getter _area2x2Getter;
        private readonly SolarBatteryFactory _solarBatteryFactory;

        public Metadata(IArea2x2Getter area2x2Getter, SolarBatteryFactory solarBatteryFactory)
        {
            _area2x2Getter = area2x2Getter;
            _solarBatteryFactory = solarBatteryFactory;
        }

        public string ObjectType => BuildingTypes.SolarBattery;

        public string Description => "Solar battery";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => _solarBatteryFactory;

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Aluminum, 20), new ResourceChunk(ResourceType.Silicon, 100));
    }
}