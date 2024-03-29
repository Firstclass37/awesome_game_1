﻿using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects._PeriodicAction;
using Game.Server.Models;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.DataBuilding
{
    internal class GameObjectAggregatorBuilder
    {
        private GameObject _gameObject;
        private List<GameObjectToAttribute> _attributes = new();
        private List<GameObjectInteraction> _interactions = new();
        private List<PeriodicAction> _periodicActions = new();
        private List<GameObjectPosition> _area = new();

        public GameObjectAggregatorBuilder(string type, int playerId)
        {
            _gameObject = new GameObject(type);
            _gameObject.PlayerId = playerId;
        }


        public GameObjectAggregatorBuilder AddArea(Coordiante root, bool blocking = false)
        {
            _area = new List<GameObjectPosition> { new GameObjectPosition(_gameObject.Id, root, true, blocking) };
            return this;
        }

        public GameObjectAggregatorBuilder AddArea(Coordiante root, Coordiante[] area, bool blocking = false)
        {
            _area = area.Select(a => new GameObjectPosition(_gameObject.Id, a, a == root, blocking)).ToList();
            return this;
        }

        public GameObjectAggregatorBuilder AddAttribute(string attributeType, object value = null)
        {
            _attributes.Add(new GameObjectToAttribute(_gameObject.Id, attributeType, value));
            return this;
        }

        public GameObjectAggregatorBuilder AddAttribute<T>(GameObjectAttribute<T> gameObjectAttribute, T value)
        {
            AddAttribute(gameObjectAttribute.Name, value);
            return this;
        }

        public GameObjectAggregatorBuilder AddInteraction<T>() where T: ICharacterInteraction
        {
            _interactions.Add(new GameObjectInteraction(_gameObject.Id, typeof(T).FullName));
            return this;
        }

        public GameObjectAggregatorBuilder AddPeriodicAction<T>(double periodSeconds) where T : IPeriodicAction
        {
            _periodicActions.Add(new PeriodicAction(_gameObject.Id, typeof(T).FullName, periodSeconds, 0));
            return this;
        }

        public GameObjectAggregator Build()
        {
            return new GameObjectAggregator
            {
                GameObject = _gameObject,
                Attributes = _attributes,
                Interactions = _interactions,
                PeriodicActions = _periodicActions,
                Area = _area
            };
        }
    }
}