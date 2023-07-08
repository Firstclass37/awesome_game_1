using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.Characters.Creation;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.Characters
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IDefaultAreaGetterFactory _defaultAreaGetterFactory;
        private readonly CharacterFactory _characterFactory;

        public Metadata(IDefaultAreaGetterFactory defaultAreaGetterFactory, CharacterFactory characterFactory)
        {
            _defaultAreaGetterFactory = defaultAreaGetterFactory;
            _characterFactory = characterFactory;
        }

        public string ObjectType => CharacterTypes.Default;

        public IAreaGetter AreaGetter => _defaultAreaGetterFactory.Get1x1();

        public ICreationRequirement CreationRequirement => null;

        public IGameObjectFactory GameObjectFactory => _characterFactory;
    }
}