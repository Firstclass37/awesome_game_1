using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.UranusMine.Creation;
using Game.Server.Logic.Objects.UranusMine.Requirement;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.UranusMine
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IDefaultAreaGetterFactory _defaultAreaGetterFactory;
        private readonly UranusMineFactory _uranusMineFactory;
        private readonly UranusMineRequirement _uranusMineRequirement;

        public Metadata(IDefaultAreaGetterFactory defaultAreaGetterFactory, UranusMineFactory uranusMineFactory, UranusMineRequirement uranusMineRequirement)
        {
            _defaultAreaGetterFactory = defaultAreaGetterFactory;
            _uranusMineFactory = uranusMineFactory;
            _uranusMineRequirement = uranusMineRequirement;
        }

        public string ObjectType => BuildingTypes.MineUranus;

        public string Description => "Uranus mine";

        public IAreaGetter AreaGetter => _defaultAreaGetterFactory.Get2x2();

        public ICreationRequirement CreationRequirement => _uranusMineRequirement;

        public IGameObjectFactory GameObjectFactory => _uranusMineFactory;

        public Dictionary<int, int> BasePrice => new Dictionary<int, int>();
    }
}