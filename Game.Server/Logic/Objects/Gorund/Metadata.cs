using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.Gorund.Creation;
using Game.Server.Logic.Objects.Gorund.Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.Gorund
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IDefaultAreaGetterFactory _areaGetterFactory;
        private readonly GroundRequirement _groundRequirement;

        public Metadata(IDefaultAreaGetterFactory areaGetterFactory, GroundRequirement groundRequirement)
        {
            _areaGetterFactory = areaGetterFactory;
            _groundRequirement = groundRequirement;
        }

        public string ObjectType => GroundTypes.Ground;

        public string Description => "Default ground";

        public Price BasePrice => Price.Free;

        public IAreaGetter AreaGetter => _areaGetterFactory.Get1x1();

        public ICreationRequirement CreationRequirement => _groundRequirement;

        public IGameObjectFactory GameObjectFactory => new GroundFactory();
    }
}