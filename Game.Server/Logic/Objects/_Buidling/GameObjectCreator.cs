using Game.Server.DataAccess;
using Game.Server.Events.Core;
using Game.Server.Events.List;
using Game.Server.Events.List.Homes;
using Game.Server.Logger;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Maps.Extentions;
using Game.Server.Logic.Objects._Core;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Buidling
{
    internal class GameObjectCreator : IGameObjectCreator
    {
        private record CreationArgs(IGameObjectMetadata Metadata, Coordiante Root, Coordiante[] Area);

        private readonly IGameObjectMetadata[] _metadatas;
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly IEventAggregator _eventAggregator;
        private readonly IAreaCalculator _areaCalculator;
        private readonly IPlayerGrid _playerGrid;
        private readonly ILogger _logger;

        public GameObjectCreator(IGameObjectMetadata[] metadatas, IGameObjectAgregatorRepository gameObjectAgregatorRepository, IGameObjectAccessor gameObjectAccessor, IEventAggregator eventAggregator, ILogger logger, IAreaCalculator areaCalculator, IPlayerGrid playerGrid)
        {
            _metadatas = metadatas;
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _gameObjectAccessor = gameObjectAccessor;
            _eventAggregator = eventAggregator;
            _logger = logger;
            _areaCalculator = areaCalculator;
            _playerGrid = playerGrid;
        }

        public bool CanCreate(CreationParams creationParams)
        {
            ArgumentNullException.ThrowIfNull(nameof(creationParams));

            try
            {
                GetCreationArgs(creationParams);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public GameObjectAggregator Create(CreationParams creationParams)
        {
            ArgumentNullException.ThrowIfNull(nameof(creationParams));

            var creationArgs = GetCreationArgs(creationParams);
            var createdObject = creationArgs.Metadata.GameObjectFactory.CreateNew(creationArgs.Root, creationArgs.Area);

            createdObject.GameObject.PlayerId = creationParams.Player;
            _gameObjectAgregatorRepository.Add(createdObject);

            PublishEvent(createdObject, creationArgs.Metadata.ObjectType, creationArgs.Root, creationArgs.Area);
            return createdObject;
        }

        private void PublishEvent(GameObjectAggregator gameObject, string objectType, Coordiante point, Coordiante[] area)
        {
            if (gameObject.GameObject.ObjectType == CharacterTypes.Default)
            {
                _eventAggregator.GetEvent<GameEvent<CharacterCreatedEvent>>()
                    .Publish(new CharacterCreatedEvent { CharacterId = gameObject.GameObject.Id, Position = point });
            }
            else
            {
                _eventAggregator.GetEvent<GameEvent<ObjectCreatedEvent>>()
                    .Publish(new ObjectCreatedEvent { Id = gameObject.GameObject.Id, ObjectType = objectType, Area = area, Root = point });
            }

            _logger.Info($"object created at {point} with area: {string.Join(";", area.Select(a => a.ToString()).ToArray())}");
        }

        private CreationArgs GetCreationArgs(CreationParams creationParams)
        {
            if (string.IsNullOrWhiteSpace(creationParams.ObjectType))
                throw new ArgumentNullException(nameof(creationParams.ObjectType));

            var metadata = _metadatas.FirstOrDefault(m => m.ObjectType == creationParams.ObjectType);
            if (metadata == null)
                throw new ArgumentException($"metadata for object {creationParams.ObjectType} was not found");

            if (_areaCalculator.TryGetArea(creationParams.Point, metadata.Size, out var originalArea) == false)
                throw new ArgumentException($"can't calculate area for {creationParams.ObjectType} for point {creationParams.Point}");

            if (creationParams.Player.HasValue && !_playerGrid.IsAvailableFor(originalArea, creationParams.Player.Value))
                throw new ArgumentException($"can't build {creationParams.ObjectType} on {creationParams.Point} couse the area is not available for player {creationParams.Player}");

            var area = originalArea.ToDictionary(a => a, a => _gameObjectAccessor.Find(a));
            if (!metadata.CreationRequirement.Satisfy(creationParams.Point, area))
                throw new Exception($"can't create object {creationParams.ObjectType} here [{creationParams.Point.X} {creationParams.Point.Y}]");

            return new CreationArgs(metadata, creationParams.Point, area.Keys.ToArray());
        }
    }
}