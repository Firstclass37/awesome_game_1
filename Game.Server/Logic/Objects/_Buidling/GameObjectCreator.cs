using Game.Server.DataAccess;
using Game.Server.Logic.Objects._Core;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Buidling
{
    internal class GameObjectCreator : IGameObjectCreator
    {
        private readonly IGameObjectMetadata[] _metadatas;
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;

        public GameObjectCreator(IGameObjectMetadata[] metadatas, IGameObjectAgregatorRepository gameObjectAgregatorRepository)
        {
            _metadatas = metadatas;
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
        }

        public GameObjectAggregator Create(string objectType, Coordiante point, object args)
        {
            var metadata = _metadatas.FirstOrDefault(m => m.ObjectType == objectType);
            if (metadata == null)
                throw new ArgumentException($"metadata for object {objectType} was not found");

            var area = metadata.AreaGetter.GetArea(point);
            if (!metadata.CreationRequirement.Satisfy(area))
                return null;

            var createdObject = metadata.GameObjectFactory.CreateNew(point, area);
            _gameObjectAgregatorRepository.Add(createdObject);
            return createdObject;
        }
    }
}