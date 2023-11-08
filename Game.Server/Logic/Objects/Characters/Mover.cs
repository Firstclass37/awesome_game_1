using Game.Server.DataAccess;
using Game.Server.Logger;
using Game.Server.Logic.Objects.Characters.Movement.PathSearching;
using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters
{
    internal class Mover : IMover
    {
        private readonly IPathSearcher _pathSearcher;
        private readonly IPathSearcherSettingsFactory _pathSearcherSettingsFactory;
        private readonly INieighborsSearchStrategy<Coordiante> _onlyRoadNeighboursSelector;
        private readonly INieighborsSearchStrategy<Coordiante> _allNeighboursSelector;
        private readonly ILogger _logger;
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;

        public Mover(IPathSearcher pathSearcher, IPathSearcherSettingsFactory pathSearcherSettingsFactory,
            IOnlyRoadNeighboursSelector onlyRoadNeighboursSelector, ILogger logger,
            IGameObjectAgregatorRepository gameObjectAgregatorRepository,
            IAllNeighboursSelector allNeighboursSelector)
        {
            _pathSearcher = pathSearcher;
            _pathSearcherSettingsFactory = pathSearcherSettingsFactory;
            _onlyRoadNeighboursSelector = onlyRoadNeighboursSelector;
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _logger = logger;
            _allNeighboursSelector = allNeighboursSelector;
        }

        public void MoveTo(GameObjectAggregator gameObject, Coordiante coordiante, Guid? initiator, bool? onlyRoadPath = true)
        {
            ArgumentNullException.ThrowIfNull(coordiante);
            ArgumentNullException.ThrowIfNull(gameObject);

            StopMoving(gameObject);

            var root = gameObject.RootCell;
            var neighborsSelector = onlyRoadPath.HasValue && onlyRoadPath.Value ? _onlyRoadNeighboursSelector : _allNeighboursSelector;
            var path = _pathSearcher.Search(root, coordiante, _pathSearcherSettingsFactory.Create(neighborsSelector));
            if (!path.Any())
            {
                _logger.Info($"PATH WAS NOT FOUND FOR {gameObject.GameObject.Id} FROM {root} to {coordiante}");
            }
            else
            {
                gameObject.SetAttributeValue(MovementAttributes.Movementpath, path);
                gameObject.SetAttributeValue(MovementAttributes.Iniciator, initiator);
                gameObject.SetAttributeValue(MovementAttributes.LastMovementTime, 0);
                gameObject.SetAttributeValue(MovementAttributes.MovingTo, null);
                _gameObjectAgregatorRepository.Update(gameObject);
            }
        }

        public void StopMoving(GameObjectAggregator gameObject)
        {
            gameObject.SetAttributeValue(MovementAttributes.Movementpath, null);
            if (!gameObject.AttributeExists(MovementAttributesTypes.MovingTo))
                gameObject.SetAttributeValue(MovementAttributes.MovingTo, null);

            //gameObject.SetAttributeValue(MovementAttributes.Iniciator, null);
            //gameObject.SetAttributeValue(MovementAttributes.MovingTo, null);
            _gameObjectAgregatorRepository.Update(gameObject);
        }
    }
}