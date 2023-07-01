using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.Home.Requirement
{
    internal class HomeCreationRequirement : ICreationRequirement
    {
        private readonly IStorage _storage;

        public HomeCreationRequirement(IStorage storage)
        {
            _storage = storage;
        }

        public bool Satisfy(Coordiante[] area)
        {
            var gameObjectsHere = _storage.Find<GameObjectPosition>(p => area.Contains(p.Coordiante)).Select(e => e.EntityId).ToArray();
            var groundAround = _storage.Find<GameObject>(o => o.ObjectType == BuildingTypes.Ground)
                .Where(o => gameObjectsHere.Contains(o.Id))
                .Count();

            return area.Count() == groundAround;
        }
    }
}