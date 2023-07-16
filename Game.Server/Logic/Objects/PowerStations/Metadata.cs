using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.PowerStations.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.PowerStations
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IDefaultAreaGetterFactory _areFactory;
        private readonly PowerStationFactory _factory;

        public Metadata(IDefaultAreaGetterFactory areFactory, PowerStationFactory homeFactory)
        {
            _areFactory = areFactory;
            _factory = homeFactory;
        }

        public string ObjectType => BuildingTypes.GeothermalStation;

        public string Description => "Power station";

        public IAreaGetter AreaGetter => _areFactory.Get2x2();

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => _factory;

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Aluminum, 100), 
            new ResourceChunk(ResourceType.Steel, 40));
    }
}