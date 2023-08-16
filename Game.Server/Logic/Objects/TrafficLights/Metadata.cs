using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.TrafficLights.Creation;
using Game.Server.Logic.Objects.TrafficLights.Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.TrafficLights
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly TrafficLightBuildRequirement _trafficLightBuildRequirement;
        private readonly TrafficLightFactory _trafficLightFactory;

        public Metadata(TrafficLightBuildRequirement trafficLightBuildRequirement, TrafficLightFactory trafficLightFactory)
        {
            _trafficLightBuildRequirement = trafficLightBuildRequirement;
            _trafficLightFactory = trafficLightFactory;
        }

        public string ObjectType => BuildingTypes.TrafficLigh;

        public string Description => "Traffic light!";

        public AreaSize Size => AreaSize.Area1x1;

        public ICreationRequirement CreationRequirement => _trafficLightBuildRequirement;

        public IGameObjectFactory GameObjectFactory => _trafficLightFactory;

        public Price BasePrice => Price.Free;
    }
}