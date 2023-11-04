﻿using Game.Server.Events.Core;
using Game.Server.Events.List.Movement;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Core;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Systems
{
    internal class InteractionSystem : ISystem
    {
        private readonly IInteractionsCollection _interactionsCollection;
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly IStorage _storage;

        public InteractionSystem(IEventAggregator eventAggregator, IGameObjectAccessor gameObjectAccessor,
            IStorage storage, IInteractionsCollection interactionsCollection)
        {
            _gameObjectAccessor = gameObjectAccessor;
            _storage = storage;
            _interactionsCollection = interactionsCollection;

            eventAggregator.GetEvent<GameEvent<GameObjectPositionChangedEvent>>().Subscribe(OnPositionChanged);
        }

        public void Process(double gameTime)
        {

        }

        private void OnPositionChanged(GameObjectPositionChangedEvent changed)
        {
            var type = _storage.Get<GameObject>(changed.GameObjectId)?.ObjectType;
            if (type == null)
                return;

            var interactWith = _gameObjectAccessor.FindAll(changed.NewPosition).FirstOrDefault(p => p.GameObject.Id != changed.GameObjectId);
            if (interactWith == null) 
                return;

            var iniciator = _gameObjectAccessor.Get(changed.GameObjectId);
            Interact(iniciator, interactWith, changed.NewPosition);

            if (!interactWith.Interactions.Any())
                return;

            var iniciatorPost  = _gameObjectAccessor.Get(changed.GameObjectId);
            var targetPost = _gameObjectAccessor.Get(interactWith.GameObject.Id);
            if (iniciatorPost != null && targetPost != null)
                Interact(targetPost, iniciatorPost, changed.NewPosition);
        }

        private void Interact(GameObjectAggregator who, GameObjectAggregator with, Coordiante interactionPoint)
        {
            var singleInteraction = who.Interactions.FirstOrDefault();
            if (singleInteraction == null)
                return;

            var interaction = _interactionsCollection.Get(singleInteraction.InteractionType);
            interaction.Interact(who, with, interactionPoint);
        }
    }
}