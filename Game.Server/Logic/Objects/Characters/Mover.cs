using Game.Server.Logger;
using Game.Server.Logic.Objects.Characters.Movement.PathSearching;
using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.Characters
{
    internal class Mover : IMover
    {
        private readonly IPathSearcher _pathSearcher;
        private readonly IPathSearcherSettingsFactory _pathSearcherSettingsFactory;
        private readonly IStorage _storage;
        private readonly IOnlyRoadNeighboursSelector _onlyRoadNeighboursSelector;
        private readonly ILogger _logger;

        private readonly Guid _autoMovementInitiator = Guid.Empty;

        public Mover(IPathSearcher pathSearcher, IPathSearcherSettingsFactory pathSearcherSettingsFactory, IStorage storage, IOnlyRoadNeighboursSelector onlyRoadNeighboursSelector, ILogger logger)
        {
            _pathSearcher = pathSearcher;
            _pathSearcherSettingsFactory = pathSearcherSettingsFactory;
            _storage = storage;
            _onlyRoadNeighboursSelector = onlyRoadNeighboursSelector;
            _logger = logger;
        }

        public Models.Movement GetCurrentMovement(Character character)
        {
            ArgumentNullException.ThrowIfNull(character);

            return _storage.Find<Models.Movement>(c => c.GameObjectId == character.Id && c.Active).FirstOrDefault();
        }

        public void MoveTo(Character character, Coordiante coordiante, Guid? initiator)
        {
            ArgumentNullException.ThrowIfNull(coordiante);
            ArgumentNullException.ThrowIfNull(character);

            StopMoving(character);

            var root = character.GameObject.RootCell;
            var path = _pathSearcher.Search(root, coordiante, _pathSearcherSettingsFactory.Create(_onlyRoadNeighboursSelector));
            if (!path.Any())
            {
                _logger.Info($"PATH WAS NOT FOUND FOR {character.Id} FROM {root} to {coordiante}");
            }
            else
            {
                _storage.Add(new Models.Movement(character.Id, path, initiator ?? _autoMovementInitiator));
            }
        }

        public void StopMoving(Character character)
        {
            var characterId = character.GameObject.GameObject.Id;
            var activeMovement = _storage.Find<Models.Movement>(c => c.GameObjectId == characterId && c.Active).FirstOrDefault();
            if (activeMovement == null)
                return;

            _storage.Remove(activeMovement);
        }
    }
}