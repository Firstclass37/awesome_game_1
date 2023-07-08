using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.TrafficLights.Creation;
using Game.Server.Logic.Objects.TrafficLights.Requirements;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.TrafficLights
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IDefaultAreaGetterFactory _areaGetterFactory;
        private readonly TrafficLightBuildRequirement _trafficLightBuildRequirement;
        private readonly TrafficLightFactory _trafficLightFactory;

        public Metadata(IDefaultAreaGetterFactory areaGetterFactory, TrafficLightBuildRequirement trafficLightBuildRequirement, TrafficLightFactory trafficLightFactory)
        {
            _areaGetterFactory = areaGetterFactory;
            _trafficLightBuildRequirement = trafficLightBuildRequirement;
            _trafficLightFactory = trafficLightFactory;
        }

        public string ObjectType => BuildingTypes.TrafficLigh;

        public string Description => "Traffic light!";

        public IAreaGetter AreaGetter => _areaGetterFactory.Get1x1();

        public ICreationRequirement CreationRequirement => _trafficLightBuildRequirement;

        public IGameObjectFactory GameObjectFactory => _trafficLightFactory;

        public Dictionary<int, int> BasePrice => new Dictionary<int, int>();
    }
}