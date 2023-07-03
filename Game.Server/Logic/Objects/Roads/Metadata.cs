using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.Roads.Createtion;
using Game.Server.Logic.Objects.Roads.Reuirements;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.Roads
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly RoadCreationRequirement _requirement;
        private readonly IDefaultAreaGetterFactory _areaGetterFactory;
        private readonly RoadFactory _roadFactory;

        public Metadata(RoadCreationRequirement requirement, IDefaultAreaGetterFactory areaGetterFactory, RoadFactory roadFactory)
        {
            _requirement = requirement;
            _areaGetterFactory = areaGetterFactory;
            _roadFactory = roadFactory;
        }

        public string ObjectType => BuildingTypes.Road;

        public IAreaGetter AreaGetter => _areaGetterFactory.Get1x1();

        public ICreationRequirement CreationRequirement => _requirement;

        public IGameObjectFactory GameObjectFactory => _roadFactory;
    }
}