using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.UranusMine.Requirement
{
    internal class UranusMineRequirement : ICreationRequirement
    {
        private readonly IStorage _storage;
        private readonly IGameObjectAccessor _gameObjectAccessor;

        public UranusMineRequirement(IStorage storage, IGameObjectAccessor gameObjectAccessor)
        {
            _storage = storage;
            _gameObjectAccessor = gameObjectAccessor;
        }

        public bool Satisfy(Coordiante[] area)
        {
            var gameObjectsIdHere = _storage.Find<GameObjectPosition>(p => area.Contains(p.Coordiante)).Select(e => e.EntityId).ToArray();
            var gameObjectsHere = gameObjectsIdHere.Select(g => _gameObjectAccessor.Get(g)).ToArray();

            var resources = gameObjectsHere
                .Where(o => o.GameObject.ObjectType == ResourceResourceTypes.Uranium)
                .ToArray();

            if (!resources.Any())
                return false;

            var grounds = gameObjectsHere
                .Except(resources)
                .Where(o => o.GameObject.ObjectType == BuildingTypes.Ground)
                .Count();

            return (resources.Count() + grounds) == gameObjectsHere.Count();
        }
    }
}
