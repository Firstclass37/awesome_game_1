using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.PowerStations.Creation;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.PowerStations
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IDefaultAreaGetterFactory _areFactory;
        private readonly PowerStationFactory _factory;
        private readonly OnlyGroundRequirement _onlyGroundRequirement;

        public Metadata(IDefaultAreaGetterFactory areFactory, PowerStationFactory homeFactory, OnlyGroundRequirement onlyGroundRequirement)
        {
            _areFactory = areFactory;
            _factory = homeFactory;
            _onlyGroundRequirement = onlyGroundRequirement;
        }

        public string ObjectType => BuildingTypes.PowerStation;

        public IAreaGetter AreaGetter => _areFactory.Get2x2();

        public ICreationRequirement CreationRequirement => _onlyGroundRequirement;

        public IGameObjectFactory GameObjectFactory => _factory;
    }
}