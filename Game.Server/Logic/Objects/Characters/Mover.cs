using Game.Server.Logic.Objects.Characters.Movement.PathSearching;
using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Models;
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

        public Mover(IPathSearcher pathSearcher, IPathSearcherSettingsFactory pathSearcherSettingsFactory, IStorage storage)
        {
            _pathSearcher = pathSearcher;
            _pathSearcherSettingsFactory = pathSearcherSettingsFactory;
            _storage = storage;
        }

        public CharacterMovement GetCurrentMovement(Character character)
        {
            return _storage.Find<CharacterMovement>(c => c.CharacterId == character.Id && c.Active).FirstOrDefault();
        }

        public void MoveTo(Character character, Coordiante coordiante, Guid? initiator)
        {
            var root = character.Position;
            var path = _pathSearcher.Search(root, coordiante, _pathSearcherSettingsFactory.Create(SelectSelector(root, coordiante)));
            if (!path.Any())
                throw new ArgumentException();

            StopMoving(character);

            var movement = new CharacterMovement
            {
                CharacterId = character.Id,
                LastMovement = 0,
                MovementIniciator = initiator,
                Path = path
            };
            _storage.Add(movement);
        }


        public void StopMoving(Character character)
        {
            var characterId = character.GameObject.GameObject.Id;
            var activeMovement = _storage.Find<Models.Movement>(c => c.GameObjectId == characterId && c.Active).FirstOrDefault();
            if (activeMovement == null)
                return;

            _storage.Remove(activeMovement);
        }

        private INieighborsSearchStrategy<Coordiante> SelectSelector(Coordiante currentPosition, Coordiante targetPosition)
        {
            return default;
            //return new OnlyRoadNeighboursSelector(_neighboursAccessor);

            //if (IsRoad(currentPosition) && IsRoad(targetPosition))
            //    return new OnlyRoadNeighboursSelector(map);
            //else
            //    return new AllNeighboursSelector(map);
        }
    }
}