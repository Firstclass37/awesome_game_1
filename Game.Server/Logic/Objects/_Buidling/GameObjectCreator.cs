using Game.Server.DataAccess;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Core;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Buidling
{
    internal class GameObjectCreator : IGameObjectCreator
    {
        private readonly IGameObjectMetadata[] _metadatas;
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;
        private readonly IGameObjectAccessor _gameObjectAccessor;

        public GameObjectCreator(IGameObjectMetadata[] metadatas, IGameObjectAgregatorRepository gameObjectAgregatorRepository, IGameObjectAccessor gameObjectAccessor)
        {
            _metadatas = metadatas;
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _gameObjectAccessor = gameObjectAccessor;
        }

        public GameObjectAggregator Create(string objectType, Coordiante point, object args)
        {
            var metadata = _metadatas.FirstOrDefault(m => m.ObjectType == objectType);
            if (metadata == null)
                throw new ArgumentException($"metadata for object {objectType} was not found");

            var area = metadata.AreaGetter.GetArea(point).ToDictionary(a => a, a => _gameObjectAccessor.Find(a));
            if (!metadata.CreationRequirement.Satisfy(area))
                throw new Exception($"can't create object {objectType} here [{point.X} {point.Y}]");

            var createdObject = metadata.GameObjectFactory.CreateNew(point, area.Keys.ToArray());
            _gameObjectAgregatorRepository.Add(createdObject);
            return createdObject;
        }
    }
}