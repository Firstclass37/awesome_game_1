﻿using Game.Server.DataAccess;
using Game.Server.Events.Core;
using Game.Server.Events.List.Homes;
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
        private readonly IEventAggregator _eventAggregator;

        public GameObjectCreator(IGameObjectMetadata[] metadatas, IGameObjectAgregatorRepository gameObjectAgregatorRepository, IGameObjectAccessor gameObjectAccessor, IEventAggregator eventAggregator)
        {
            _metadatas = metadatas;
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _gameObjectAccessor = gameObjectAccessor;
            _eventAggregator = eventAggregator;
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
            _eventAggregator.GetEvent<GameEvent<ObjectCreatedEvent>>()
                .Publish(new ObjectCreatedEvent { Id = createdObject.GameObject.Id, ObjectType = objectType, Area = area.Keys.ToArray(), Root = point });
            return createdObject;
        }
    }
}