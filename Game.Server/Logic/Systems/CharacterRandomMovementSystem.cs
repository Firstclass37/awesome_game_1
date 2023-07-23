using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Logic.Objects.Characters.Movement.PathSearching;
using Game.Server.Storage;
using Game.Server.Logger;
using Game.Server.Models.Maps;
using Game.Server.Models;
using Game.Server.Logic.Maps;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;

namespace Game.Server.Logic.Systems
{
    internal class CharacterRandomMovementSystem : ISystem
    {
        private readonly IPathSearcher _pathSearcher;
        private readonly IPathSearcherSettingsFactory _pathSearcherSettingsFactory;
        private readonly IMapGrid _mapGrid;
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly IStorage _storage;
        private readonly ILogger _logger;

        public CharacterRandomMovementSystem(IPathSearcher pathSearcher, IPathSearcherSettingsFactory pathSearcherSettingsFactory,
            IMapGrid mapGrid, IGameObjectAccessor gameObjectAccessor, IStorage storage, ILogger logger)
        {
            _pathSearcher = pathSearcher;
            _pathSearcherSettingsFactory = pathSearcherSettingsFactory;
            _mapGrid = mapGrid;
            _gameObjectAccessor = gameObjectAccessor;
            _storage = storage;
            _logger = logger;
        }

        public void Process(double gameTime)
        {
            var characters = _storage.Find<GameObject>(o => o.ObjectType == CharacterTypes.Default)
                .Where(c => _storage.Find<Movement>(m => m.GameObjectId == c.Id).All(m => m.Active == false))
                .ToArray();

            foreach (var character in characters)
                MoveToRandomPoint(character.Id);
        }

        private void MoveToRandomPoint(Guid characterId)
        {
            var randomPoint = _mapGrid.GetGrid()
                .OrderBy(g => Guid.NewGuid())
                .First(p => _gameObjectAccessor.Find(p)?.GameObject.ObjectType == BuildingTypes.Road);

            var character = _gameObjectAccessor.Get(characterId);
            var initialPosition = character.Area.First().Coordiante;

            var path = _pathSearcher.Search(initialPosition, randomPoint, _pathSearcherSettingsFactory.Create(SelectSelector(initialPosition, randomPoint)));
            if (path.Any())
            {
                _storage.Add(new Movement(characterId, path, Guid.Empty));
            }
            else
            {
                _logger.Info($"PATH WAS NOT FOUND FOR {characterId} FROM {initialPosition} to {randomPoint}");
            }
        }

        private INieighborsSearchStrategy<Coordiante> SelectSelector(Coordiante currentPosition, Coordiante targetPosition)
        {
            return new OnlyRoadNeighboursSelector(_mapGrid, _gameObjectAccessor);
        }
    }
}