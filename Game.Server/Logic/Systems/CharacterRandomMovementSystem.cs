﻿using Game.Server.Storage;
using Game.Server.Models;
using Game.Server.Logic.Maps;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Logic._Extentions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Systems
{
    internal class CharacterRandomMovementSystem : ISystem
    {
        private readonly IMapGrid _mapGrid;
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly IStorage _storage;
        private readonly IMover _mover;
        private readonly Guid _autoMovementInitiator = Guid.Parse("2CC3B290-6A0F-497C-ADB1-409A8D662A32");

        public CharacterRandomMovementSystem(IGameObjectAccessor gameObjectAccessor, IStorage storage, IMover mover, IMapGrid mapGrid)
        {
            _gameObjectAccessor = gameObjectAccessor;
            _storage = storage;
            _mover = mover;
            _mapGrid = mapGrid;
        }

        private double LastFireTime = 0;

        public void Process(double gameTime)
        {
            var needFire = gameTime - LastFireTime > 1;
            if (needFire == false)
                return;

            var characters = _storage.Find<GameObject>(o => o.ObjectType == CharacterTypes.Default)
                .Where(c => _storage.Find<Movement>(m => m.GameObjectId == c.Id).All(m => m.Active == false))
                .ToArray();

            foreach (var character in characters)
                MoveToRandomPoint(character.Id);

            LastFireTime = gameTime;
        }

        private void MoveToRandomPoint(Guid characterId)
        {
            var randomPoint = _mapGrid.GetGrid()
                .OrderBy(g => Guid.NewGuid())
                .Select(p => new { Coordinate = p, Object = _gameObjectAccessor.Find(p) })
                .Where(p => p.Object != null)
                .First(p => p.Object.GameObject.ObjectType == BuildingTypes.Road || p.Object.Interactable())
                .Coordinate;

            var character = _gameObjectAccessor.Get(characterId);
            _mover.MoveTo(new Models.GamesObjectList.Character(character), randomPoint, _autoMovementInitiator);
        }
    }
}