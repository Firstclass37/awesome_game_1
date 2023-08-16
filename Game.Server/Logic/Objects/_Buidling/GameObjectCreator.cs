using Game.Server.DataAccess;
using Game.Server.Events.Core;
using Game.Server.Events.List;
using Game.Server.Events.List.Homes;
using Game.Server.Logger;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Core;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Buidling
{
    internal class GameObjectCreator : IGameObjectCreator
    {
        private readonly IGameObjectMetadata[] _metadatas;
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly IEventAggregator _eventAggregator;
        private readonly IAreaCalculator _areaCalculator;
        private readonly ILogger _logger;

        public GameObjectCreator(IGameObjectMetadata[] metadatas, IGameObjectAgregatorRepository gameObjectAgregatorRepository, IGameObjectAccessor gameObjectAccessor, IEventAggregator eventAggregator, ILogger logger, IAreaCalculator areaCalculator)
        {
            _metadatas = metadatas;
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _gameObjectAccessor = gameObjectAccessor;
            _eventAggregator = eventAggregator;
            _logger = logger;
            _areaCalculator = areaCalculator;
        }

        public bool CanCreate(string objectType, Coordiante point, object args = null)
        {
            if (string.IsNullOrWhiteSpace(objectType)) 
                throw new ArgumentNullException(nameof(objectType));

            var metadata = _metadatas.FirstOrDefault(m => m.ObjectType == objectType);
            if (metadata == null)
                throw new ArgumentException($"metadata for object {objectType} was not found");

            if (GetArea(point, metadata.Size, out var originalArea) == false)
                return false;

            var area = originalArea.ToDictionary(a => a, a => _gameObjectAccessor.Find(a));
            return metadata.CreationRequirement.Satisfy(point, area);
        }

        public GameObjectAggregator Create(string objectType, Coordiante point, object args = null)
        {
            if (string.IsNullOrWhiteSpace(objectType))
                throw new ArgumentNullException(nameof(objectType));

            var metadata = _metadatas.FirstOrDefault(m => m.ObjectType == objectType);
            if (metadata == null)
                throw new ArgumentException($"metadata for object {objectType} was not found");

            if (GetArea(point, metadata.Size, out var originalArea) == false)
                throw new ArgumentException($"can't calculate area for {objectType} for point {point}");

            var area = originalArea.ToDictionary(a => a, a => _gameObjectAccessor.Find(a));
            if (!metadata.CreationRequirement.Satisfy(point, area))
                throw new Exception($"can't create object {objectType} here [{point.X} {point.Y}]");

            var createdObject = metadata.GameObjectFactory.CreateNew(point, area.Keys.ToArray());
            _gameObjectAgregatorRepository.Add(createdObject);
            PublishEvent(createdObject, objectType, point, area.Keys.ToArray());
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
                    .Publish(new ObjectCreatedEvent { Id = gameObject.GameObject.Id, ObjectType = objectType, Area = area.ToArray(), Root = point });
            }

            _logger.Info($"object created at {point} with area: {string.Join(";", area.Select(a => a.ToString()).ToArray())}");
        }

        private bool GetArea(Coordiante root, AreaSize areaSize, out Coordiante[] area) 
        {
            area = Array.Empty<Coordiante>();

            try
            {
                area = _areaCalculator.GetArea(root, areaSize);
                return true;
            }
            catch 
            {
                return false;
            }   
        }
    }
}