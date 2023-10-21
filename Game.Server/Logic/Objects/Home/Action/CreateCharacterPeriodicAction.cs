using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._PeriodicAction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Home.Action
{
    internal class CreateCharacterPeriodicAction : IPeriodicAction
    {
        private readonly IGameObjectCreator _gameObjectCreator;
        private readonly IGameObjectAccessor _gameObjectAccessor;

        public CreateCharacterPeriodicAction(IGameObjectCreator gameObjectCreator, IGameObjectAccessor gameObjectAccessor)
        {
            _gameObjectCreator = gameObjectCreator;
            _gameObjectAccessor = gameObjectAccessor;
        }

        public void Trigger(GameObjectAggregator gameObject)
        {
            var spawnCell = gameObject.GetAttributeValue<Coordiante>(AttributeType.SpawnCell);
            var spawnObject = _gameObjectAccessor.Find(spawnCell);
            if (spawnObject.GameObject.ObjectType == BuildingTypes.Road)
                _gameObjectCreator.Create(new CreationParams(CharacterTypes.Default, spawnCell));
        }
    }
}